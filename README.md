# HapticForceCalculationAlgorithm
*author : wanglin*

*date: 2023-5-11*
## I.Overview

- I.`Voxelization` and `Surface Packing`  (fill with `inner sphere`)
  - I. we need to create a `boundingbox` arround the `complex mesh` , and confirm that the mesh is `closed`
  - II. grid the `boudingbox` according to the `customized specified resolution` (box size)
  - III. determine whether the `voxel` (segmented box) is inside or outside the `complex mesh` , just like `SDF`,the `outside voxels `were `eliminated`
  - IV. we need to calculate a `distance map` (we could use `physical raycast` from unity,if not ,`BVH` instead)
    - we need to find the `face` which the voxel is `closest` to the  `complex mesh`,and calculate the distance
    - iterate each voxel
  - V. fill the `complex mesh` with `sphere collider`
    - because the `inner sphere` rotation is constant,alter the transform only affect the position
    - we use a `greedy` algorithm
    - First,we choose the voxel `farthest` from the face to fill a `largest ball`,the radius is the voxel to the face
    - then,Step by step filling
    - so,the `collision complexity` is related to the number of `sphere collider`
    - because of the `loss of significance`,there are requirements for the `thickness` of the `grid division` and the size of the `sphere collider`, which need to be `balanced`
    - finally, we filled the `complex mesh` with `sphere collider` large and small
- II.Calculate the haptic force
  - I. we need a `IST` (`inner sphere tree`),different from `BVH`
  - II. however ,we use `Unity` ,so ,IST is not necessary. 
  - III. finally,we calculate the 6-DOF `forces`, `torques`, etc. based on the `previous overlap` of the `sphere collider`s, the `count`, etc
  - IV. we choose this algorithm because of we need `500-1000hz` speed
