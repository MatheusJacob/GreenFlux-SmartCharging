###  GreenFlux-SmartCharging-Matheus
Assignment made to GreenFlux to create a simple API to control capacity on Smart Charging

### Install
```bash
 $ brew install httpie
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

