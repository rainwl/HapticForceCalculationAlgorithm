# HapticForceCalculationAlgorithm
*author : wanglin*

*date: 2023-5-11*
## I.Overview

- I.`Voxelization` and `Surface Packing`  (fill with `inner sphere`)
  - [x] I. we need to create a `boundingbox` arround the `complex mesh` , and confirm that the mesh is `closed`
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
## II.Implement
### II.I How to divide the bounds
#### I. `Signed Distance Fields`
A Signed Distance Field (SDF) is a 3D texture representation of mesh geometry. To represent the geometry, each texel stores the closest distance value to the surface of the mesh. By convention, this distance is negative inside the mesh and positive outside. This texture representation of the mesh enables you to place a particle at any point on the surface, inside the bounds of the geometry, or at any given distance to it.

I iterate the SDF by ComputeBuffer,create a computeShader and define a computeBuffer to iterate SDF and save the result.I use computeShader.SetBuffer() and .Dispatch() to save and call computeShader. In computeShader, I use thread id and group id to iterate SDF data(make sure that every thread could call on SDF data).Result has been writen in OutputBuffer,and use .Getdata() to write in output buffer and save to disk.

However,there's some exceptions . such as the fileBytes 's length is "(Multiple of 4) + 1",and property (Result) at kernel index (0) is not set ,etc.

So,I 'd like to use another way to implement this func,and when I have been done this thing,I will come back to solve these problems.

#### II. `Voxelization technology based on Octree`
Octree is a hierarchical structure that divides a three-dimensional space into a series of octree nodes, each of which represents a cube volume and is subdivided into smaller child nodes as needed.Using Octree's voxelization technology can improve efficiency while maintaining high quality voxelization.

#### III. `Native SDF`
Evaluate signed-distance-fields with great efficiency using the power of the Unity Job System and the Burst Compiler.

## III.Calculate
### III.I theory
The direction of the penalty force can be derived from the weighted average of all vectors between the centers of colliding pairs of shperes,weighted by their overlap.

A simple heuristic would be to consider all overlapping pairs of spheres separately.

Penetration volume of two spheres with radius r1 and r2 respectively.

![](https://pic4rain.oss-cn-beijing.aliyuncs.com/img/71FC2EF3-992C-4116-8081-95F59BA50AFF.png)

![](https://pic4rain.oss-cn-beijing.aliyuncs.com/img/BDC69D2B-9A72-4075-8D0B-F4A66BFEC1D1.png)

![](https://pic4rain.oss-cn-beijing.aliyuncs.com/img/17D12242-1589-4c6d-8061-3A60FF09BD29.png)

![](https://pic4rain.oss-cn-beijing.aliyuncs.com/img/8C1020EC-869D-4b88-A00F-9651954128F4.png)

### III.II step

- Common Func for calculate penalty force between two spheres
- iterate all of the spheres and sum the force up
  -  iterate from the dynamic cylinder
  -  check for null,if not,calculate



## Statistics
In Unity2019,Graphics can run as 6000 fps

but in Unity2021,only 300 fps

![](https://pic4rain.oss-cn-beijing.aliyuncs.com/img/%E5%B1%8F%E5%B9%95%E6%88%AA%E5%9B%BE%202023-05-15%20174609.png)
