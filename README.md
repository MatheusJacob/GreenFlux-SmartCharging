###  GreenFlux-SmartCharging-Matheus [![Build Status](https://travis-ci.com/MatheusJacob/GreenFlux-SmartCharging.svg?branch=main)](https://travis-ci.com/MatheusJacob/GreenFlux-SmartCharging)
Assignment made to GreenFlux to create a simple API to control capacity on Smart Charging

### Install
```bash
 $ dotnet restore
 $ dotnet test
 $ dotnet run --project src\GreenFlux.SmartCharging.Matheus.API\ --urls=http://localhost:5001
 ```
 
 ### Swagger
 
 ```
 http://localhost:5001/swagger/
 ```
### Time spent
-  2% creating the visual studio solution with all references and architectural layers;
- 10% understanding the requirement;
- 5% designing the solution;
- 30% coding the solution;
- 1% creating the source repository and doing the right source control;
- 52% testing the solution.

### Improvements
- Adding a fallback when connectors > 100 on the suggestion generation function, a good fallback here would be to generate subarrays instead of subsets,would sacrifice a little bit the results efficiency, but would scale way better.
- Decoupling the DB Context from the controller, adding interfaces to the controller instead of the actual implementation
- Adding a service layer to be able to reuse methods from the controller, the current state is still manageable because there are only 3 controllers, but if the system starts to add more routes would start to have a lot of duplicated code on the controller.
- Instead of returning all connectors from a group to calculate the current sum, we could save the whole structure as a fenwick tree, so there's no need to recalculate everything, only the leafs affected on the operation(as fenwick trees are not good at inserting new nodes, we could just save the connectors with empty values on charge station creation, and later just update the value)

### Initial Thoughts
- I kept a log with ideas and thoughts while I was developing the project on the readme.md file history, you can check it here [Readme History](https://github.com/MatheusJacob/GreenFlux-SmartCharging/commits/main/README.md)
