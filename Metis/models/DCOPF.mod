
set GENERATORS;
set GENPARAMS;
set LINES;
set TIME;
set LineParams;
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

var P{GENERATORS,TIME} >= 0;
var P_N {GENERATORS, LIN_SEGMENTS, TIME} >= 0;
var PL {LINES, TIME};
var Theta {BUSES, TIME};


minimize Total_Cost:
	sum {i in GENERATORS, t in TIME, s in LIN_SEGMENTS} (
	GenData ['MC', i] 
	+ Slope [s,i] * P_N[i,s,t]
	);
	
subject to Total_Power {i in GENERATORS,t in TIME}:
	P[i,t] = GenData['Pmin',i]  + sum { s in LIN_SEGMENTS } P_N[i,s,t]
	;

subject to Max_Aux {i in GENERATORS, t in TIME, s in LIN_SEGMENTS}:
	P_N[i,s,t] <= Len[s,i]
	;

subject to Max_P {i in GENERATORS, t in TIME}:
	P[i,t] <= GenData['Pmax', i] 
	;
		
subject to Min_P {i in GENERATORS, t in TIME}:
	P[i,t] >= GenData['Pmin', i] 
	;
	
subject to Power_Balance {b in BUSES, t in TIME}:
	sum {i in GENERATORS} (P[i,t] * GenToBus[b, i])  - Demand[b, t] = sum {l in LINES} (LineToBus[b, l] * PL[l, t]);
	;
	
subject to Line_Flow {t in TIME, l in LINES}:
	PL[l, t] = 100 * sum{b in BUSES}(Theta[b, t] * LineToBus[b, l]) / LineData['x',l];
	;

subject to Max_PL {t in TIME, l in LINES}:
	PL[l, t] <= LineData['PLmax', l];
	;	

subject to Min_PL {t in TIME, l in LINES}:
	PL[l ,t] >= - LineData['PLmax', l];
	;
		