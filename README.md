# InVisionAR App

This is the repository with the Unity code for the main InVisionAR app.

## Development Enviornment Requirements

- Git
- ***Git LFS***
- Visual Studio Code
- Unity 2020.1.4f1
  - This is highly subject to change, so please update your Unity installation as neccesary.
  - Remember to install Android Build Support
  - Remember to install iOS build support(only do this if you are on a Mac and have an iPhone)
- Android Studio
- XCode(only on Mac if you have an iPhone)
- A phone with AR support(either an Android with ARCore support or an iPhone). Not required, but will make your job much easier.

## Setting up the devlopment environment

1. Fork this repository
1. Clone the fork you made using `git clone` under your account. **(Make sure you have Git LFS installed)**
1. `cd` into the folder and keep the terminal open. Add the project to Unity(make sure the version is correct) and open the project.
1. Set VSCode as the default editor for Unity(make sure VSCode is installed before you do this)
1. Also open the project in VSCode(you can do this either by opening a script file from Unity or by opening VSCode seperately and opening the project folder)
1. Open the VSCode terminal with `` Ctrl + `  ``
1. Run `git remote add upstream https://github.com/InVisionAR/web-frontend.git` to set the root repository as the upstream remote.
1. Create a new branch with `git branch new-branch-name`
   - If the new branch is for a new feature, prefix the branch name with `feature/`. For example, `feature/login-system`
   - If the new branch name is a bug fix, prefix the branch name with `bugfix/`. For example, `bugfix/signup-error`
1. Switch to the new branch with `git checkout new-branch-name`.
1. Make a small change relavent to your task(like creating a new scene or a script file), add the changes with `git add .`, commit the changes with `git commit -m "Intitial commit for <task-name>"` and push your changes with `git push origin new-branch-name`
1. Create a pull request from your branch to the master branch of the main repository**(not the master of your repository)**
1. Start working on your task!
1. Stage and commit changes regularly with `git add .` and `git commit -m "commit message"`, respectively.
1. Push your changes regularly with `git push origin new-branch-name`
1. Pull changes from upstream master regulary with `git pull upstream master`. **Run step 6 after pulling from upstream master**. If there are merge conflicts, ask for help.
1. When you are finished working on a task, ask for code review from Sagar Patil.
1. Wait for code review.
1. Fix things according to review feedback.
1. If code review is passed, your branch will be merged. Go to step 6 and start working your next task. If your review did not pass, go back to step 15.
