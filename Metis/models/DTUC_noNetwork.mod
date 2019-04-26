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
var I {GENERATORS, TIME} binary;


#--------------------------------------------------------------------------------------
#---------------------------------------- OBJECTIVE -----------------------------------
#--------------------------------------------------------------------------------------

minimize Total_Cost:
	sum {i in GENERATORS, t in TIME} (
	GenData ['MINCOST', i] * I[i,t] * GenData['FUEL',i]
	+ sum {s in LIN_SEGMENTS}( Slope [s,i] * P_N[i,s,t] * GenData['FUEL',i])
	+ C_SU [i,t] * GenData['FUEL',i]
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
	
subject to Power_Balance {t in TIME}:
	sum {i in GENERATORS} (P[i,t])  = Load[t];
	;

#----------------------------------------------------------------------------		
	
subject to ST_Cost {i in GENERATORS, t in TIME: t > 1}:
	C_SU[i, t] >= GenData['STCOST', i] * (I[i, t] - I[i, t-1])
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




