# Exercise 4 Limit Check DataReference
A custom AFDataReference to check compliance of a measurement AFAttribute against it's Limit Traits.  

# Calculated Results
The output from the custom data reference will be an AFValue contain an Int32 in its .Value property.  This calculated Int32 represents 1 of 5 possible states as shown in the table below:

| Int32 | Meaning |
| :---: | :--- |
| -2 | LoLo violation |
| -1 | Lo violation but not LoLo |
| 0 | No limits violated, i.e. Normal |
| 1 | Hi violation but not HiHi |
| 2 | HiHi violation |

In general, states are best thought of as being disassociated from a number scale.  However, the notion of Lo versus Hi has a natural affinity for the Y-axis where Lo denotes _something less than_ and Hi denotes _something greater than_.  The specific numbering of the state codes was chosen to reflect this physical relationship to the Y-axis.

What can you do with this raw Int32 later in PI AF, particularly with PI System Explorer?  First, you may trend it as time-series data where the resulting trend will have a proper visual representation of lows versus highs.  Another thing you can do, and are in fact _encouraged to do_, is to create AFEnumerationSets to map the state code into a more meaningful phrase.  This could be quite generic, for example "HiHi", or could be a wee bit more specific such as "Critical High".  Or you may want the text to be very specific.  For example, if you were dealing with ambient temperature you may want to display "Hot" or "Very Hot".

# License

Copyright 2017 OSIsoft, LLC

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the specific language governing permissions and limitations under the License.
