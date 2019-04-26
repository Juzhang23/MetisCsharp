
set GENERATORS;
set GENPARAMS;
set LINES;
set LineParams;
set TIME ordered;
set TTIME := TIME;
set BUSES;
set LIN_SEGMENTS;

param GenData {GENPARAMS,GENERATORS};
param Len {LIN_SEGMENTS, GENERATORS};
param Slope {LIN_SEGMENTS, GENERATORS};
param GenToBus{BUSES,GENERATORS};
param LineToBus {BUSES,LINES};
param LineData {LineParams,LINES};
param Load {TIME};
param Demand {BUSES, TIME} >= 0;
param Copy_GenData {GENPARAMS,GENERATORS};

var P{GENERATORS,TIME} >= 0;
var P_N {GENERATORS, LIN_SEGMENTS, TIME} >= 0;
var C_SU {GENERATORS, TIME} >= 0;
var	C_SD {GENERATORS, TIME} >= 0;
var I {GENERATORS, TIME} binary;
var PL {LINES, TIME};
var Theta {BUSES, TIME} >= -3.14, <= 3.14; 


#--------------------------------------------------------------------------------------
#---------------------------------------- OBJECTIVE -----------------------------------
#--------------------------------------------------------------------------------------

minimize Total_Cost:
	sum {i in GENERATORS, t in TIME, s in LIN_SEGMENTS} (
	GenData ['MC', i] * I[i,t]
	+ Slope [s,i] * P_N[i,s,t]
	+ C_SU [i,t]
	+ C_SD [i,t]
	);
#--------------------------------------------------------------------------------------
#---------------------------------------- CONSTRAINTS ---------------------------------
#--------------------------------------------------------------------------------------
	
subject to Total_Power {i in GENERATORS,t in TIME}:
	P[i,t] = GenData['Pmin',i] * I[i,t] + sum { s in LIN_SEGMENTS } P_N[i,s,t]
	;

subject to Max_Aux {i in GENERATORS, t in TIME, s in LIN_SEGMENTS}:
	P_N[i,s,t] <= Len[s,i]
	;

subject to Max_P {i in GENERATORS, t in TIME}:
	P[i,t] <= GenData['Pmax', i] * I[i,t]
	;
		
subject to Min_P {i in GENERATORS, t in TIME}:
	P[i,t] >= GenData['Pmin', i] * I[i,t]
	;
	
subject to Power_Balance {b in BUSES, t in TIME}:
	sum {i in GENERATORS} (P[i,t] * GenToBus[b, i])  - Demand[b, t] = sum {l in LINES} (LineToBus[b, l] * PL[l, t]);
	;
#--------------------------------------- LINE FLOW ---------------------------------------	
subject to Line_Flow {t in TIME, l in LINES}:
	PL[l, t] = 100 * sum{b in BUSES}(Theta[b, t] * LineToBus[b, l]) / LineData['x',l];
	;

subject to Max_PL {t in TIME, l in LINES}:
	PL[l, t] <= LineData['PLmax', l];
	;	

subject to Min_PL {t in TIME, l in LINES}:
	PL[l ,t] >= - LineData['PLmax', l];
	;
#----------------------------------------------------------------------------		
	
subject to ST_Cost {i in GENERATORS, t in TIME}:
	C_SU[i, t] >=
	if t == 1 then
		GenData['K', i] * (I[i, t] - GenData['INI', i])
	else
		GenData['K', i] * (I[i, t] - I[i, t-1])
	;

subject to SD_Cost {i in GENERATORS, t in TIME}:
	C_SD[i, t] >= 
	if t == 1 then
		GenData['J', i] * (GenData['INI', i] - I[i, t])
	else
		GenData['J', i] * (I[i, t-1] - I[i, t])
	;
	
subject to RampUp  {i in GENERATORS, t in TIME : t > 1}:

	P[i,t] - P [i, t-1] <= GenData['RU',i] * I[i,t-1] 
							+ GenData['SUR',i] * (I[i,t] - I[i,t-1]) 
							+ GenData['Pmax',i] * (1 - I[i,t]) 
	;

subject to RampDown  {i in GENERATORS, t in TIME: t > 1}:
	P[i,t-1] - P [i, t] <= GenData['RD',i] * I[i,t] 
							+ GenData['SDR',i] * (I[i,t-1] - I[i,t]) 
							+ GenData['Pmax',i] * (1 - I[i,t-1]) 
	;

subject to MinUpTime {i in GENERATORS, t in 2..card(TIME) - GenData['Ton',i] + 1 }:
	sum {tt in t..(t + GenData['Ton',i] -1)} 
		(I[i,tt]) 
	>=
	GenData['Ton',i] * (I[i,t] - I[i,t-1])
	;

subject to MinDownTime {i in GENERATORS, t in 2..card(TIME) - GenData['Toff',i] + 1 }:
	sum {tt in t..(t + GenData['Toff',i] -1)} 
		(1- I[i,tt]) 
	>=
	GenData['Toff',i] * (I[i,t-1] - I[i,t])
	;


