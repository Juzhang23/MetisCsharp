set GENERATORS;
set GENPARAMS;
set ES_PARAMS;

set TIME ordered;
set TTIME := TIME;
set TIME_AF24H ordered;

set LIN_SEGMENTS;
set DIS_LIN_SEG;
set CH_LIN_SEG;

set HERM_COEF ordered;
set HERM_COEF_D4 ordered;

set BERN_COEF_D2 ordered;
set BERN_COEF_Dp2 ordered;
set BERN_COEF_D3 ordered;
set BERN_COEF_D4;

set ENERGY_ST;
set G_TYPE;

set r ordered;
set c;

param O {BERN_COEF_D3, BERN_COEF_D2};
param OO {BERN_COEF_D2, BERN_COEF_Dp2};
param O_hat {BERN_COEF_D3, HERM_COEF_D4};
param W {HERM_COEF, BERN_COEF_D3};
param Copy_GenData {GENPARAMS,GENERATORS};

param GenData {GENPARAMS,GENERATORS};
param ES_Data {ENERGY_ST, ES_PARAMS};
param Copy_ES_Data {ENERGY_ST, ES_PARAMS};
param Len {LIN_SEGMENTS, GENERATORS};
param Slope {LIN_SEGMENTS, GENERATORS};

param RTP {GENERATORS};
param N_H {TIME, HERM_COEF};
param L_B {BERN_COEF_D3, TIME};
param Lp_B {BERN_COEF_D2, TIME}; 
param CostRate {GENERATORS,LIN_SEGMENTS};

param LG {ENERGY_ST, DIS_LIN_SEG};
param LD {ENERGY_ST, CH_LIN_SEG};
param CG {ENERGY_ST, DIS_LIN_SEG};
param CD {ENERGY_ST, CH_LIN_SEG};

param Load{r,c};
param RSC;

var G_H_S {ENERGY_ST, TIME, HERM_COEF};
var D_H_S {ENERGY_ST, TIME, HERM_COEF};
var Gp_B_S {ENERGY_ST, TIME, BERN_COEF_D2};
var Dp_B_S {ENERGY_ST, TIME, BERN_COEF_D2};
var E_B_S {ENERGY_ST, TIME, HERM_COEF_D4};
var Gamma_h {GENERATORS,LIN_SEGMENTS, HERM_COEF, TIME};
var Gp_b {GENERATORS,TIME,BERN_COEF_D2};			# Bernstein coefficient of the first derivative of the unit i'th generation at time t
var G_B  {GENERATORS,TIME,BERN_COEF_D3};			# Bernstein coefficient of the continuous-time generation trajectory of unit i at hourly interval t           
var G_H  {GENERATORS,TIME,HERM_COEF};				# Hermite coefficient of the continuous-time generation trajectory of unit i at hourly interval t
var G_B_S {ENERGY_ST, TIME, BERN_COEF_D3} >= 0;
var D_B_S  {ENERGY_ST, TIME, BERN_COEF_D3} >= 0;
var W_ES_G {ENERGY_ST, TIME, DIS_LIN_SEG, BERN_COEF_D3} >= 0;
var W_ES_D {ENERGY_ST, TIME, CH_LIN_SEG, BERN_COEF_D3} >= 0;
var C_G_ES >= 0;
var C_D_ES >= 0;
var W_G  {GENERATORS, LIN_SEGMENTS, TIME, BERN_COEF_D3} >= 0; #Auxiliary variable for generation coordinate of joint cost function 
var C_SU {GENERATORS, TIME} >= 0;					#Start-up cost of the generation unit i at time interval t    (HeatRate)  
var I {GENERATORS, TIME} binary;


#--------------------------------------------------------------------------------------
#---------------------------------------- OBJECTIVE -----------------------------------
#--------------------------------------------------------------------------------------

minimize Total_Cost:
	sum {i in GENERATORS, t in TIME} (C_SU [i,t] * GenData['FUEL', i] + GenData ['MINCOST', i] * GenData['FUEL', i] * I[i,t])
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
	sum {i in GENERATORS} (G_H[i, t, h]) + sum {e in ENERGY_ST} (G_H_S[e,t,h] - D_H_S[e,t,h]) = N_H[t,h]	
	;

subject to Load_Balance2	{t in TIME, h in HERM_COEF: (ord(h) >= 3) and (ord(t) == card(TIME))}:
	sum {i in GENERATORS} (G_H[i, t, h]) + sum {e in ENERGY_ST} (G_H_S[e,t,h] - D_H_S[e,t,h]) = N_H[t,h]	
	;
#--------------------------------------- Continuity Constraints ---------------------------------------	

subject to Continuity1		{i in GENERATORS,t in TIME: ord(t) <> card(TIME)}: 
	G_H [i,t+1,'H1'] = G_H [i,t,'H3']
	;
subject to Continuity2		{i in GENERATORS,t in TIME: ord(t) <> card(TIME)}: 
	G_H [i,t+1,'H2'] = G_H [i,t,'H4']
	;
subject to Continuity3		{e in ENERGY_ST,t in TIME: ord(t) <> card(TIME)}:
	G_H_S[e,t+1,'H1'] = G_H_S[e,t,'H3']
	;
subject to Continuity4		{e in ENERGY_ST,t in TIME: ord(t) <> card(TIME)}:
	G_H_S[e,t+1,'H2'] = G_H_S[e,t,'H4']
	;
subject to Continuity5		{e in ENERGY_ST,t in TIME: ord(t) <> card(TIME)}:
	D_H_S[e,t+1,'H1'] = D_H_S[e,t,'H3']
	;
subject to Continuity6		{e in ENERGY_ST,t in TIME: ord(t) <> card(TIME)}:
	D_H_S[e,t+1,'H2'] = D_H_S[e,t,'H4']
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

subject to ES_maxP_ch		{e in ENERGY_ST,t in TIME, b in BERN_COEF_D3}:
	D_B_S[e,t,b] <= ES_Data[e, 'Pmax_ch']
	;

subject to ES_maxP_dis		{e in ENERGY_ST,t in TIME, b in BERN_COEF_D3}:
	G_B_S[e,t,b] <= ES_Data[e, 'Pmax_dis']
	;

subject to ES_maxE		{e in ENERGY_ST,t in TIME, bi in HERM_COEF_D4}:
	E_B_S[e,t,bi] <= ES_Data[e, 'Emax']
	;

subject to ES_minE		{e in ENERGY_ST,t in TIME, bi in HERM_COEF_D4}:
	E_B_S[e,t,bi] >= ES_Data[e, 'Emin']
	;
	
#--------------------------------------- Hermite to Bernstein Conversion ---------------------------------------	

subject to Bernstein1		{i in GENERATORS, t in TIME, b in BERN_COEF_D3}:
	G_B[i,t,b] = sum { h in HERM_COEF} (W[h,b] * G_H[i,t,h])
	;
subject to Bernstein2		{e in ENERGY_ST, t in TIME, b in BERN_COEF_D3}:
	G_B_S[e,t,b] = sum { h in HERM_COEF} (W[h,b] * G_H_S[e,t,h])
	;
subject to Bernstein3		{e in ENERGY_ST, t in TIME, b in BERN_COEF_D3}:
	D_B_S[e,t,b] = sum { h in HERM_COEF} (W[h,b] * D_H_S[e,t,h])
	;
#--------------------------------------- First Derivative Coefficients ---------------------------------------	

subject to First_Deriv1		{i in GENERATORS, t in TIME, bp in BERN_COEF_D2}:
	Gp_b [i,t, bp] = sum { b in BERN_COEF_D3} (O [b,bp] * G_B[i,t,b])
	; 
	
subject to First_Deriv2		{e in ENERGY_ST, t in TIME, bp in BERN_COEF_D2}:
	Gp_B_S [e,t, bp] = sum { b in BERN_COEF_D3} (O [b,bp] * G_B_S[e,t,b])
	;
	 
subject to First_Deriv3		{e in ENERGY_ST, t in TIME, bp in BERN_COEF_D2}:
	Dp_B_S [e,t, bp] = sum { b in BERN_COEF_D3} (O [b,bp] * D_B_S[e,t,b])
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
	
subject to ES_maxRU_ch		{e in ENERGY_ST, t in TIME, bp in BERN_COEF_D2}:
	Dp_B_S[e,t,bp] <= ES_Data[e,'RU_S']
	;
	
subject to ES_maxRD_ch		{e in ENERGY_ST, t in TIME, bp in BERN_COEF_D2}:
	-Dp_B_S[e,t,bp] <= ES_Data[e,'RD_S']
	; 
	
subject to ES_maxRU_dis		{e in ENERGY_ST, t in TIME, bp in BERN_COEF_D2}:
	Gp_B_S[e,t,bp] <= ES_Data[e,'RU_S']
	; 
	
subject to ES_maxRD_dis		{e in ENERGY_ST, t in TIME, bp in BERN_COEF_D2}:
	-Gp_B_S[e,t,bp] <= ES_Data[e,'RU_S']
	;  
	
#--------------------------------------- Energy Bernstein Conversion ---------------------------------------	

subject to Integral1		{e in ENERGY_ST, t in TIME, bi in HERM_COEF_D4: ord(t)<>1}:
	E_B_S[e,t,bi] = sum {b in BERN_COEF_D3}
						(
						O_hat[b,bi] * (ES_Data[e,'eta_ch'] * D_B_S[e,t,b] - (1/(ES_Data[e,'eta_dis'])) * G_B_S[e,t,b])
						)
					+ E_B_S[e,t-1, 'HD5']
	;

subject to Integral1_INI	{e in ENERGY_ST, t in TIME, bi in HERM_COEF_D4: ord(t) == 1}:
	E_B_S[e,t,bi] = sum {b in BERN_COEF_D3}(O_hat[b,bi] * (ES_Data[e,'eta_ch'] * D_B_S[e,t,b] - (1/(ES_Data[e,'eta_dis'])) * G_B_S[e,t,b]))
					+ ES_Data[e, 'E0']
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

subject to Aux_ES_1			{e in ENERGY_ST, t in TIME, b in BERN_COEF_D3}:
	G_B_S[e,t,b] = sum {ns in DIS_LIN_SEG} (W_ES_G[e,t,ns,b])	
	;

subject to Aux_ES_2			{e in ENERGY_ST, t in TIME, b in BERN_COEF_D3}:
	D_B_S[e,t,b] = sum {hs in CH_LIN_SEG} (W_ES_D[e,t,hs,b])	
	; 

subject to Aux_ES_3			{e in ENERGY_ST, t in TIME, ns in DIS_LIN_SEG,  b in BERN_COEF_D3}:
	W_ES_G[e,t,ns,b] <= LG[e,ns]
	;

subject to Aux_ES_4			{e in ENERGY_ST, t in TIME, hs in CH_LIN_SEG,  b in BERN_COEF_D3}:
	W_ES_D[e,t,hs,b] <= LD[e,hs]
	;
#--------------------------------------------------------------------------------------
#---------------------------------------- END -----------------------------------------
#--------------------------------------------------------------------------------------

