Exercise 2 - Create a WinForm Weather Applet

There is no change needed in the AFDatabase at the end of Exercise 1.

This exercise has a lot of moving parts to it.  While intended for a beginner to AF SDK, it is expected that the learner is a developer experienced in WinForms and .NET.  If you lack experience with Microsoft common controls such as list box, combo box, group box, radio buttons, or command buttons, you may struggle with this exercise.

There are many ways to complete the exercise.  The solution provided here is but one example.  There may be more bells and whistles than originally asked for, but is always nice to provide some fun stuff to stimulate ideas.  A listbox is provided to display the data values, though for Exercise 3, which builds upon this solution, a data grid is used for some variety.

One pain point working with form UI is having the form be aware of possible settings among the various controls, and that includes invalid settings as well.  The exercise required that you validate the user picks before getting data.  One way to do this would be to validate the choices when the user clicks the "Get Data" button.  The solution provided employs an additional approach by only enabling the "Get Data" button if all choices are valid.  That is to say the user cannot click on "Get Data" if any user choice is invalid.
