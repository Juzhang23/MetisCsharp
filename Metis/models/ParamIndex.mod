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

param GenData {GENPARAMS,GENERATORS};
param Copy_GenData {GENPARAMS,GENERATORS};
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

