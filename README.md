# GreenFlux-SmartCharging
Repo for a Assignment for GreenFlux


Initial thoughts :
-Use SQLite in memory to avoid extra configuration/setup
-Thought a little bit about using Redis but decided this is going to be a stretch goal, with Redis I would be able to control
slightly better Race conditions using WATCH, and the whole project would be ready to scale horizontally.KISS for now
-Started doing some research about Ampere and which data type precision is necessary for storing Current, came to a conclusion 
that float should be enough, if more precision is needed we can use double
-Also was thinking about which data structure works best for this, came to a conclusion that n-ary tree would be a good fit, 
because then I wouldn't need to recalculate the whole tree on every add operation,just the branch was affected, and as the tree would
be sorted would be quite easy to get the lowest connectors and remove them to accomodate a new one, but again this implementation 
would be more needed if I knew exactly the amount of charging stations that usually has in a group, if the number isn't that big, 
this implementation wouldn't have that much benefits....also this would have a downside of recalculating the parent node, which could
lead to some locks on the database.
-Decided that I'm going to follow again a KISS approach, and I'm going to create the implementation using n-ary tree after the project 
is finished
-I'm not a big fan of TDD, but as the scope is very well defined I think tdd would be a perfect fit here

-Initial API structure :
GET     /api/groups
POST    /api/groups
GET     /api/groups/:groupId
DELETE  /api/groups/:groupId

GET     /api/groups/:groupId/chargeStations
POST    /api/groups/:groupId/chargeStations
PATCH   /api/groups/:groupId/chargeStations/:chargeStationId
DELETE  /api/groups/:groupId/chargeStations/:chargeStationId

GET     /api/groups/:groupId/chargeStations/:chargeStationId/connectors
POST    /api/groups/:groupId/chargeStations/:chargeStationId/connectors
PATCH   /api/groups/:groupId/chargeStations/:chargeStationId/connectors/:connectorId
DELETE  /api/groups/:groupId/chargeStations/:chargeStationId/connectors/:connectorId

-Initial Project structure :
/API
--/Controllers/
/Domain
--/Interfaces/
--/Entities/
--/Services/
/Persistence
--/Repository/
/Tests
--/Integration/
--/Unit/
