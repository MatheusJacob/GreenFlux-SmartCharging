# GreenFlux-SmartCharging
Repo for a Assignment for GreenFlux


Initial thoughts :
-Use SQLite in memory to avoid extra configuration/setup
-Thought a little bit about using Redis but decided this is going to be a stretch goal, with Redis I would be able to control
slightly better Race conditions using WATCH, and the whole project would be ready to scale horizontally.KISS for now
-Started doing some research about Ampere and which data type precision is necessary for storing Current, came to a conclusion 
that float should be enought, if more precision is needed we can use double

-Initial API structure :
GET     /api/groups
POST    /api/groups
GET     /api/groups/:groupId
DELETE  /api/groups/:groupId

GET     /api/groups/:groupId/chargeStations
POST    /api/groups/:groupId/chargeStations
PATCH   /api/groups/:groupId/chargeStations/:chargeStationId
DELETE  /api/groups/:groupId/chargeStations/:chargeStationId

GET     /api/groups/:groupId/chargeStations:chargeStationId/connectors
POST    /api/groups/:groupId/chargeStations/connectors
PATCH   /api/groups/:groupId/chargeStations/:chargeStationId/connectors/:connectorId
DELETE  /api/groups/:groupId/chargeStations/:chargeStationId/:connectorId
