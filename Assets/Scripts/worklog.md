#### OVERALL IDEAS AND LOG

# TASKS
- [ ] Make arm sway upwards
- [ ] Make sway while walking better 
- [ ] Walking sideways head tilt
- [ ] Turn animation
- [ ] Walking sideways animation
- [l] AI Integration

# IDEA
- [ ] Maybe change the settings class to localinstead of static? (New instance for each player instead of being global)
- [ ] Maybe for the camera

# BUGS
- [ ] Double jump after walking (idle->jump->walk mid jump)  

#### LOG

# 2025-01-22
Started working on the project on my pc, the whle migration thing. Other than that i made some basic animation work to make them less vomit inducing, and started a bit of work on the sideways walking animations

# 2025-01-23
NOTE: AI

Thought about the idea of making my owm machine learning model (in c or c++), or at least try. Ithought about 2 models one as personal assistant(maybe not possible), other as game direcotor (something optional and really lightweight)

# 2025-02-11

#### Overview
First log after longer break, going back to working on the engine and game.

# 2025-02-14

#### Overview
Making the soulslike controller. And for this making main scripts more modular (to make themmore flexible for random shit like this). Mainly moving the settings into the seperate function and seperation of different behaiours (like for example camera movement) into their seperate functions.

# 2025-02-15

#### Overview
More work on the souls-like controller. Added animations for souls-like player and new animator. Some materials work to make it look a bit less stupid.

#### Physics
- Changing of stupid lerp functions for accelerated walk/run into something else
- Because of this removed interpolationStep from input
- New ground check with spherecast

#### Materials
- Changes on knight materials 
- Learning a bit about mapping of unity textures

#### Animations
- SL player animations
- New jump animation implementation
