# Iesa Wazeer CS310 Final Project

This is our final year project for the year 2020/2021

## Running the game on Unity

Download and Install Unity 2019.4.12f1
Open the package in Unity

## Running the training

Download and install Python 3.6.1 or higher

Use the package manager [pip](https://pip.pypa.io/en/stable/) 

(Recommended) Create a Virtual Environment
Install PyTorch using:
 ```console
pip3 install torch~=1.7.1 -f https://download.pytorch.org/whl/torch_stable.html
```
Install the mlagent package using:

```console
python -m pip install mlagents==0.26.0
```


## Usage


The Final Game folder contains the Fighter.exe file. This runs the game with the most advanced brain we were able to create.

To test the multiple brains we created:

1. Open the Fighter folder in Unity.
2. In the Unity Hierarchy open Fight Scene > Environment > Enemy
3. In the inspector scroll down to "Behaviour Parameters"
4. From the project directory within Unity open Assets > Brains
5. Drag any of the brains (the .onnx) files into the "model" section in the inspector
6. Click play to play against that AI

To run training:

1. Activate your Virtual Environment
2. Run the game using the Unity Game Engine
3. Go to the Fighter base directory and use the command 
```console
mlagents-learn config/FighterAI.yaml --run-id Test
```
4. Training has begun and you can use Cntrl + C to stop the training

If any other errors please refer to the UnityMLAgents [documents](https://github.com/Unity-Technologies/ml-agents/tree/main/docs). For installation issues check the [installation document](https://github.com/Unity-Technologies/ml-agents/blob/main/docs/Installation.md)
