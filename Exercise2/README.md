# Exercise 2 - Create a WinForm Weather Applet

There is no change needed in the AFDatabase at the end of Exercise 1.

This exercise has a lot of moving parts to it.  While intended for a beginner to AF SDK, it is expected that the learner is an experienced developer with WinForms and .NET.  If you lack experience with Microsoft WinForm such as list box, combo box, group box, radio buttons, or command buttons, you may struggle with this exercise.

There are many ways to complete the exercise.  The solution provided here is but one example.  There may be more bells and whistles than originally asked for, but is always nice to provide some fun stuff to stimulate ideas.  A ListBox is provided to display the data values, though for Exercise 3, we substitute a DataGridView for the sake of variety.

One pain point working with a form UI is having the form be aware of possible settings among the various controls, and that includes being aware of invalid settings as well.  The exercise required that you validate the user picks before getting data.  One way to do this would be to validate the choices when the user clicks the "Get Data" button.  The solution provided employs an additional approach by only enabling the "Get Data" button only if all choices are valid.  That is to say the user cannot click on "Get Data" if any selections are invalid.
