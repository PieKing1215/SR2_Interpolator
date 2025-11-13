# Interpolator

Mod for [Slime Rancher 2](https://store.steampowered.com/app/1657630/Slime_Rancher_2/) that fixes choppy/"low fps" physics objects & first person vac animation

The mod does 2 things:
- When any dynamic Rigidbody is spawned, it enables Unity's built in interpolation on it (which is off in vanilla)
  - Affects all physics objects: slimes, food, plorts, hens, resources, etc
- Adjusts the first person vac smoothing to be softer, in vanilla it's orders of magnitude stronger than it needs to be

None of this should have any gameplay impact, though it hasn't been \*super\* thoroughly tested

### Comparison (60fps video slowed down)
| Vanilla | w/ Interpolator |
|-|-|
|<img src="https://github.com/PieKing1215/SR2_Interpolator/blob/main/media/before_slime.gif" height=300 />|<img src="https://github.com/PieKing1215/SR2_Interpolator/blob/main/media/after_slime.gif" height=300 />|
|<img src="https://github.com/PieKing1215/SR2_Interpolator/blob/main/media/before_run.gif" height=200 />|<img src="https://github.com/PieKing1215/SR2_Interpolator/blob/main/media/after_run.gif" height=200 />|
