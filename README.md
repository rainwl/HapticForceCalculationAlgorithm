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



## IV.Calulate theory II

- when a `sphere collider` 's `OnTriggerEnter` is called,record the `current position`
  - do a research about performance between `trigger` and `collision`
  - and calculate the value between `current position` and `previous position`
  - in `OnTriggerEnter`,record the position (previous position)
  - in `OnTriggerStay` ,calculate the value between pre. and cur.
  - because there's a lot of `sphere collider`,so,everyone need to calculte
  - in `OnTriggerExit`,clear the `previous position` up 
  - because the box we collide,has some sphere colliders,inevitably, there are gaps
  - so,we need to handle this

### Thinking

![](https://pic4rain.oss-cn-beijing.aliyuncs.com/img/overview.png)

![](https://pic4rain.oss-cn-beijing.aliyuncs.com/img/prevPos2.png)

![](https://pic4rain.oss-cn-beijing.aliyuncs.com/img/calculate.png)

![](https://pic4rain.oss-cn-beijing.aliyuncs.com/img/second%20frame.png)

![](https://pic4rain.oss-cn-beijing.aliyuncs.com/img/multi2.png)

![](https://pic4rain.oss-cn-beijing.aliyuncs.com/img/sum.png)


- [x] we create two collider `collections`
- [x] when a  `sphere collider` `First` `OnTriggerEnter`,record the position
  - [x] Notice:`First Enter` `record once`
- [x] in a frame:calculate the distance
  - [x] each sphere collider `previousPos` ,like graph `Right` view
- [x] in second frame,distance is `a` or `b` ?
  - [x] I choose `a` now (yes)
  - [x] need a discuss
- [x] we need to create 2 collider (mesh collider) for `CubeCollection `and `CylinderCollection`
  - [x] and we use this collider collide for `bool` control
  - [x] so far,frame keep `4000-4500fps`,collide minimum `2500fps`
- [x] if we use `=` instead of `+=` ,the force will lower down to 0 automatically (yes)
- [x] `NO` when `first sphere collider` in cylinder collection collide the `any sphere collider` box collection
  - [x] we should make cylinder collection mesh `static` there
- [ ] we should use `cylinder` collide `Box` to static it 
  - [ ] and we should use `OnCollisionEnter` to get the `collision hit` info
  - [ ] later we will draw the force at the hit postion 


```C#
    private void OnTriggerStay(Collider other)
    {
        // I choose distance "a" 
        var currPos = transform.position;
        var delta = currPos - prevPos;
        // if there need "+=" ? use "=" instead ?
        //TotalDistance += delta.magnitude;
        TotalDistance = delta.magnitude;
    }
```

## V.High-Performance
Writing multithreaded code can provide high-performance benefits.
These include significant gains in frame rate.
Using the Burst compiler with C# jobs gives you improved code generation quality.
think later~
## Refactor Theory
![](https://pic4rain.oss-cn-beijing.aliyuncs.com/img/frameALL.png)

![](https://pic4rain.oss-cn-beijing.aliyuncs.com/img/frame0.png)
- `Box Collection` : 10 * 10 * 10  `SphereColliders` with radius `0.5f` and `Is Trigger` is `on`
- `Cylinder Collection`:2 * 2 * 20 `SphereColliders` with radius `0.25f` , `Rigid body` is contained and `Is Kinematic` is `on`
- `Box Collection` is static 
- `Cylinder Collection` is dynamic

![](https://pic4rain.oss-cn-beijing.aliyuncs.com/img/frame1.png)
- If and only if the `SphereColliders` in the `Cylinder Collection` and the `SphereColliders` in the `Box Collection` first collide
  - Record the position of all `SphereColliders` in the `Cylinder Collection` and write it to the `Dictionary<Sphere Collider,Vector3>`
  -  bool `IsInitialCollision` = true
  - When there are not any collisions in the scene,`IsInitialCollision` = false
  - Record the `vectorOrigin` : collider1.center - collider2.center

![](https://pic4rain.oss-cn-beijing.aliyuncs.com/img/frame2.png)
- When some `SphereColliders` in `Cylinder Collection` penetrates into `Box Collection`
- I mark them with blue in graph
- Create a List&lt;Sphere Collider> named `collisionList`
- In `OnTriggerStay`
  - Check if this exists in `collisionList`
  - //such as , in frame 2,`vector_frame_2` and `vector_origin` are reversed ,and this case can be added to the collisionList
  - collisionList.Add(this)
  - record the previous frame postion and current position
- In `CollisionManager`
  - iterate collisionList 
  - Determine the direction of the motion vector of each `sphere collider` and the original vector in the current frame
  - if not ,remove it
  - and sum the `direction` and `distance`

![](https://pic4rain.oss-cn-beijing.aliyuncs.com/img/frame3.png)
- We can see that in the third frame, the `Cylinder Collection` falls back, and the motion vector of the `sphere collider` identified in the figure at this time is `frame3`
- `frame3` and `origin` are in the same direction, so this `sphere collider` is removed from the `collisionList`

![](https://pic4rain.oss-cn-beijing.aliyuncs.com/img/frame3case2.png)
- In this case,everything goes easy

![](https://pic4rain.oss-cn-beijing.aliyuncs.com/img/frame4.png)
- Although some of the `sphere collider`s are out of the `Box Collection` zone
- they are still in the `collisionList` and reverse the `origin`, so the force continues to be calculated

![](https://pic4rain.oss-cn-beijing.aliyuncs.com/img/frame5.png)
- Even though all the `sphere collider`s are out of the `Box Collection` zone
- they are still in the `collisionList` and reverse the `origin`, so the force continues to be calculated

![](https://pic4rain.oss-cn-beijing.aliyuncs.com/img/frame6.png)
- In this case , `Cylinder Collection` has been rotated,and some of `sphere collider`s have been gone back,some reversed
- such as,the `sphere collider` which keep vector `frame6_1`,has the same direciton of `origin`
- // and we will remove it from the `collisionList` 
- we could not remove ir from the list,because when it rotate next ,we will could not calculate it
- so ,just substract its distance 
- the operation `Remove`,just could be called at the time:
- exit from the collider and go as the same direction with origin