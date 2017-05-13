# Intelligent-PacMan
Multi-programmed ghosts using different pathfinding algorithms. Automatic movement for PacMan as well as user-controlled.

This is the class project done for CS4820 - Artificial Intelligence at UCCS 2017

PHASE 1
The first phase of the project was to program the ghosts to behave differently according to certain pathfinding algorithms.

Decision points were placed on every intersection of the board as a marker for  spots where a ghost can make a change in movement.

Pinky (pink ghost) was programmed similarly to the traditional A* algorithm. Pinky locates PacMan, goes to that position in the quickest path possible, then recalculates PacMan's position from there. (Similarly to Pinky in the actual game, however, Pinky actually looks at the decision point one ahead of PacMan in attempts to cut in front of him rather than his precise location). In order for Pinky not to get stuck, she will also recalculate after ten moves if PacMan's location is not found.

Blinky (red ghost) behaves more like the Dynamic A* algorithm. He recalculates PacMan's position at every decision point so is thinking more quickly and adapting to PacMan's movements. He goes straight for PacMan himself in the shortest path from each new decision point.

Inky (blue ghost) and Clyde (orange ghost) work together using the Cooperative A* algorithm. With this algorithm, agents are not allowed to pass through one another, so these two ghosts will turn around if they attempt to move towards a decision point which the other ghost is also heading to. In order for them not to behave exactly the same, rather than choosing the shortest path at each decision point, they make a random move with higher probabilitiy of taking the shortest path. Inky has a slightly better chance of making the 'correct' move than Clyde with more probability given to the shortest path for him.


PHASE 2
The second phase of the project was to program PacMan himself. PacMan behaves in one of two ways. If he is close enough to a ghost to be 'concerned', he will take the quickest path away from that ghost until he is far enough to 'relax'. If he is far enough from the closest ghost to not worry about it, he will look in each direction for the closest Pacdot using a raycast (so, in a sense, he can only see the dots 'visible' to him at his current location and not any dots around corners). With Blinky and Pinky, this allows for PacMan to get stuck at positions where he isn't close enough to a ghost to be worried and doesn't see any Pacdots nearby.


PacMan was tested 10 times for Inky and Clyde and once for Blinky and Pinky (since the behavior was exactly the same each time for them due to the lack of randomness). The number of Pacdots collected was documented and compared with previous tests of the same ghosts against human players. In all cases, the programmed PacMan performed more than twice as good as the average human player.

Results for the experiments are given in the CS4820Final document.
