# InVisionAR App

This is the repository with the Unity code for the main InVisionAR app.

## Development Enviornment Requirements

- Git
- **Git LFS**
- Visual Studio Code
- Unity 2020.1.4f1
  - This is highly subject to change, so please update your Unity installation as neccesary.
  - Remember to install Android Build Support
  - Remember to install iOS build support(only do this if you are on a Mac)
- Android Studio
- XCode(only on Mac)

## Setting up the devlopment environment

1. Fork this repository
2. Clone the fork you made using `git clone` under your account. **(Make sure you have Git LFS installed)**
3. `cd` into the folder and keep the terminal open. Add the project to Unity(make sure the version is correct) and open the proejct
4. Run `git remote add upstream https://github.com/InVisionAR/web-frontend.git` to set the root repository as the upstream remote.
5. Create a new branch with `git branch new-branch-name`
   - If the new branch is for a new feature, prefix the branch name with `feature/`. For example, `feature/login-system`
   - If the new branch name is a bug fix, prefix the branch name with `bugfix/`. For example, `bugfix/signup-error`
6. Switch to the new branch with `git checkout new-branch-name`.
7. Run `npm install`. This might take a while.
8. Run `npm start` inside the folder to start a devlopment server.
9. Start working!
10. Stage and commit changes regularly with `git add <relevant files>` and `git commit -m "commit message"`, respectively.
11. Push your changes regularly with `git commit origin new-branch-name`
12. Pull changes from upstream master regulary with `git pull upstream master`. **Run step 6 after pulling from upstream master**. If there are merge conflicts, ask for help.
13. When you are ready to push your changes to the root master, log on to GitHub.com and create a pull request from your branch(`new-branch-name`, for exmaple) to the master branch of the main repository.
14. Wait for code review.
15. Fix things according to review feedback.
16. If code review is passed, your branch will be merged. Go to step 6 and start working your next task. If your review did not pass, go back to step 15.
