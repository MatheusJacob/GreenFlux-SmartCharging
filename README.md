# GreenFlux-SmartCharging
Repo for a Assignment for GreenFlux


Initial thoughts :
-Use SQLite in memory to avoid extra configuration/setup
-Thought a little bit about using Redis but decided this is going to be a stretch goal, with Redis I would be able to control
slightly better Race conditions using WATCH, and the whole project would be ready to scale horizontally.KISS for now
-Started doing some research about Ampere and which data type precision is necessary for storing Current, came to a conclusion 
that float should be enought, if more precision is needed we can use double