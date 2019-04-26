set GENERATORS;
set GENPARAMS;
set TIME ordered;
set TTIME := TIME;
set LIN_SEGMENTS;
set HERM_COEF ordered;
set BERN_COEF_D2 ordered;
set BERN_COEF_D3 ordered;
set BERN_COEF_D4;

param O {BERN_COEF_D3, BERN_COEF_D2};
param W {HERM_COEF, BERN_COEF_D3};
param GenData {GENPARAMS,GENERATORS};
param Len {LIN_SEGMENTS, GENERATORS};
param Slope {LIN_SEGMENTS, GENERATORS};
param N_H {TIME, HERM_COEF};
param Copy_GenData {GENPARAMS,GENERATORS};

param L_B {BERN_COEF_D3, TIME};
param Lp_B {BERN_COEF_D2, TIME}; 
 
var Gp_b {GENERATORS,TIME,BERN_COEF_D2};			# Bernstein coefficient of the first derivative of the unit i'th generation at time t
var G_B  {GENERATORS,TIME,BERN_COEF_D3};			# Bernstein coefficient of the continuous-time generation trajectory of unit i at hourly interval t           
var G_H  {GENERATORS,TIME,HERM_COEF};				# Hermite coefficient of the continuous-time generation trajectory of unit i at hourly interval t
var W_G  {GENERATORS, LIN_SEGMENTS, TIME, BERN_COEF_D3} >= 0; #Auxiliary variable for generation coordinate of joint cost function 
var C_SU {GENERATORS, TIME} >= 0;					#Start-up cost of the generation unit i at time interval t    (HeatRate)  
var I {GENERATORS, TIME} binary;


#--------------------------------------------------------------------------------------
#---------------------------------------- OBJECTIVE -----------------------------------
#--------------------------------------------------------------------------------------

minimize Total_Cost:
	sum {i in GENERATORS, t in TIME} 
	(
		C_SU [i,t] * GenData['FUEL', i]
		+ GenData ['MINCOST', i] * GenData['FUEL', i] * I[i,t]
	)
	+ 0.25 * sum {i in GENERATORS, t in TIME, s in LIN_SEGMENTS, b in BERN_COEF_D3}
			 (
			 	Slope [s,i] * GenData['FUEL', i] * W_G [i,s,t,b]
			 )
	;
#--------------------------------------------------------------------------------------
#---------------------------------------- CONSTRAINTS ---------------------------------
#--------------------------------------------------------------------------------------

subject to Startup_Cost {i in GENERATORS, t in TIME: ord(t) <> 1}:
	C_SU [i,t] >= GenData['STCOST',i] * (I[i,t] - I[i, t-1])
	;
#--------------------------------------- Balance Constraints ---------------------------------------	

subject to Load_Balance1	{t in TIME, h in HERM_COEF: ord(h) <= 2}:
	sum {i in GENERATORS} (G_H[i, t, h]) = N_H[t,h]	
	;

subject to Load_Balance2	{t in TIME, h in HERM_COEF: (ord(h) >= 3) and (ord(t) == card(TIME))}:
	sum {i in GENERATORS} (G_H[i, t, h]) = N_H[t,h]	
	;
#--------------------------------------- Continuity Constraints ---------------------------------------	

subject to Continuity1		{i in GENERATORS,t in TIME: ord(t) <> card(TIME)}: 
	G_H [i,t+1,'H1'] = G_H [i,t,'H3']
	;
subject to Continuity2		{i in GENERATORS,t in TIME: ord(t) <> card(TIME)}: 
	G_H [i,t+1,'H2'] = G_H [i,t,'H4']
	;

#--------------------------------------- Generation Capacity Constraints ---------------------------------------	

subject to Pmin_g1			{i in GENERATORS, t in TIME, b in BERN_COEF_D3: ord(b) <= 2}:
	G_B[i,t,b] >= GenData['Pmin',i] * I[i,t]
	;

subject to Pmin_g2			{i in GENERATORS, t in TIME, b in BERN_COEF_D3: (ord(b) >= 3) and (ord(t) < card(TIME))}:
	G_B[i,t,b] >= GenData['Pmin',i] * I[i,t+1]
	; 	

subject to Pmin_g_END		{i in GENERATORS, t in TIME, b in BERN_COEF_D3: (ord(b) >= 3) and (ord(t) == card(TIME))}:
	G_B[i,t,b] >= GenData['Pmin',i] * I[i,t]
	;

subject to Pmax_g1			{i in GENERATORS, t in TIME, b in BERN_COEF_D3: ord(b) <= 2}:
	G_B[i,t,b] <= GenData['Pmax',i] * I[i,t]
	;

subject to Pmax_g2			{i in GENERATORS, t in TIME, b in BERN_COEF_D3: (ord(b) >= 3) and (ord(t) < card(TIME))}:
	G_B[i,t,b] <= GenData['Pmax',i] * I[i,t+1]
	; 	

subject to Pmax_g_END		{i in GENERATORS, t in TIME, b in BERN_COEF_D3: (ord(b) >= 3) and (ord(t) == card(TIME))}:
	G_B[i,t,b] <= GenData['Pmax',i] * I[i,t]
	;

#--------------------------------------- Hermite to Bernstein Conversion ---------------------------------------	

subject to Bernstein		{i in GENERATORS, t in TIME, b in BERN_COEF_D3}:
	G_B[i,t,b] = sum { h in HERM_COEF} (W[h,b] * G_H[i,t,h])
	;
#--------------------------------------- First Derivative Coefficients ---------------------------------------	

subject to First_Deriv		{i in GENERATORS, t in TIME, bp in BERN_COEF_D2}:
	Gp_b [i,t, bp] = sum { b in BERN_COEF_D3} (O [b,bp] * G_B[i,t,b])
	; 
	
#--------------------------------------- Ramping Constraints ---------------------------------------	

subject to Ramp1			{i in GENERATORS, t in TIME, bp in BERN_COEF_D2: (ord(bp) == 1) and (ord(t) > 1)}:
	Gp_b [i,t, bp] <= GenData['RU',i] * I[i,t-1]
						+ GenData['SUR',i] * (I[i,t] -I[i,t-1])
						+ GenData['Pmax',i] * (1 - I[i,t]) 
	;

subject to Ramp2			{i in GENERATORS, t in TIME, bp in BERN_COEF_D2: (ord(bp) == 1) and (ord(t) > 1)}:
	-Gp_b [i,t, bp] <= GenData['RD',i] * I[i,t]
						+ GenData['SDR',i] * (I[i,t-1] -I[i,t])
						+ GenData['Pmax',i] * (1 - I[i,t-1]) 
	;  

subject to Ramp3			{i in GENERATORS, t in TIME, bp in BERN_COEF_D2: ord(bp) == 2}:
	Gp_b [i,t, bp] <= GenData['RU',i] * I[i,t]
						+ 3 * GenData['Pmax',i] * (1 - I[i,t]) 
	;

subject to Ramp4			{i in GENERATORS, t in TIME, bp in BERN_COEF_D2: (ord(bp) == 2) and (ord(t) <> card(TIME))}:
	-Gp_b [i,t, bp] <= GenData['RD',i] * I[i,t+1]
						+ 3 * GenData['Pmax',i] * (1 - I[i,t+1]) 
	; 
#--------------------------------------- Cost Linearization ---------------------------------------	

subject to Aux_G			{i in GENERATORS, t in TIME, s in LIN_SEGMENTS, b in BERN_COEF_D3}:
	W_G [i,s,t,b] <= Len[s,i]
	;

subject to Gen_ToT1			{i in GENERATORS, t in TIME, b in BERN_COEF_D3: ord(b) <= 2}:
	G_B[i,t,b] = GenData['Pmin',i] * I[i,t]
					+ sum {s in LIN_SEGMENTS} W_G[i,s,t,b]
	;
	
subject to Gen_ToT2			{i in GENERATORS, t in TIME, b in BERN_COEF_D3: (ord(b) >= 3) and (ord(t) < card(TIME))}:
	G_B[i,t,b] = GenData['Pmin',i] * I[i,t+1]
					+ sum {s in LIN_SEGMENTS} W_G[i,s,t,b]
	;

subject to Gen_ToT_End		{i in GENERATORS, t in TIME, b in BERN_COEF_D3: (ord(b) >= 3) and (ord(t) == card(TIME))}:
	G_B[i,t,b] = GenData['Pmin',i] * I[i,t]
					+ sum {s in LIN_SEGMENTS} W_G[i,s,t,b]
	;	
#--------------------------------------------------------------------------------------
#---------------------------------------- END -----------------------------------------
#--------------------------------------------------------------------------------------

