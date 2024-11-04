# TODO:

* Automate path locations to static callables dependent on enviroment status so Testing can be done outside of the actual game data. ie saving and loading data from file. 
* create and use RewardTable.json instead of creating jobs in code (in RewardService.cs)

* remove singleton pattern for player. The main form is already a singleton and the player can be used from there. this would simplify handling player's and its service's data. 

* Create a login form to handle multiple players / characters.

* Disconnect all form methods from non forms classes. (this contradicts the point above, but it is a good practice to separate the UI from the logic (for testing))