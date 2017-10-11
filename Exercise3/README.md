# Exercise 3 - Adding Notifications Viewer on top of Exercise 2

This exercise picks up right where Exercise 2 left off.  You will need to add some objects to your AFDatabase before you modify the code.

## Required Changes

Obviously, your database will need a Notification Rule Template in order to see notification instances (event frames).  But a Notification Rule Template will need an Asset Analysis Template for generating event frames.  And to generate event frames, you will need an event frame template.

Examples of needed objects may be found in the file named **Weather Exercise 3.xml**.  Import as a new AFDatabase.  You should then backfill some analyses since yesterday.  Once that is done, the event frames should be visible in the new form added to this exercise.

As for the code solution, the only required change was to add a new form to display notification instances.  That would be a new form object in the project.  The 2 lines of code to open the new form as a dialog is at the very bottom of MainForm.  The bulk of your efforts to complete this exercise would be in the new form itself.

## Optional Changes for Variety

However, if you compare the solution from Exercise 2 to Exercise 3, the MainForm looks _very_ different.  In order to expose the learner to a wider variety of techniques and possibilities, there were some optional changes to Exercise 3 that weren't required by the instructions.  There are 2 major changes and 1 minor one.

First off, you will notice a new layout to MainForm.  The groupboxes previously in the upper right are now in the lower left.  While this layout looks very different, this would be the _minor_ change!  Repositioning controls without changing the underlying code is really just minor, although your eyeballs may disagree.

One major change is the replacing of the AFTreeView control with an **AFElementFindCtrl**.  The AFTreeView is capable of showing so many more objects within an AFDatabase.  It could show Element Templates, Categories, Tables, etc.  But for this exercise all we are only interested in is elements, and in particular, one element at a time.  The AFElementFindCtrl will let us find one element at a time, and it does so in much smaller screen space than a tree view.  Due to this space savings, we could move the other user picks to the left hand side of the form, and let the data values take up the right hand side.

The second major change was replacing the ListBox with a **DataGridView**.  Both of these are common Microsoft controls, so you should be able to find plenty of help at [MSDN](https://msdn.microsoft.com/) or other popular code sites.  The DataGridView offers a spreadsheet-style view of your data, which is a quite natural fit for displaying time-series data.

## Be Creative

Whether you use a ListBox or a DataGridView, or a AFTreeView or an AFElementFindCtrl, the important thing when writing applications, and especially UI applications, is that there are a staggering number ways you can achieve it.  Be open to trying new ideas.  Remember that the online course had this statement:  _We encourage you to get more creative and use the exercises only as pointers_.
