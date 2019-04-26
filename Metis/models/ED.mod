
set GENERATORS;
set GENPARAMS;

param GenData {GENPARAMS,GENERATORS} >= 0;

param load >=0;

var P {GENERATORS} >= 0; 

minimize Total_Cost:

	sum {i in GENERATORS} (GenData['A',i] 
	+ GenData['B', i] * P[i] 
	+ GenData['C', i] * P[i] * P[i] );

subject to Power_Balance:
	sum {i in GENERATORS} P[i] = load;

subject to Min_Power {i in GENERATORS}:
	P[i] >= GenData['Pmin',i];

subject to Max_Power {i in GENERATORS}:
	P[i] <= GenData['Pmax',i];