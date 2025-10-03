# Virtual

Unique compiler and runtime error messages, enriched with details where available.

1. Argument 1: cannot convert from 'string' to 'int'  
   - **Code:** CS1503  
   - **File:** MainForm.cs  
   - **Line:** 178, 207, 429  
   - **Reason:** The target method parameter is defined as an `int`, but the call site is passing in a `string` literal or property without converting it.  
   - **Fix:** Update the call to parse the string to an integer (e.g., `int.Parse`/`int.TryParse`) or change the method signature to accept a string if that is the intended type.

2. 'LootManager' does not contain a definition for 'Create'  
   - **Code:** CS0117  
   - **File:** MainForm.cs  
   - **Line:** 184  
   - **Reason:** The `LootManager` class lacks a `Create` member, so the call is referencing a method that was never implemented or has a different name/signature.  
   - **Fix:** Implement a `Create` method on `LootManager` with the expected signature or replace the call with the correct existing factory/initialization method.

3. Argument 1: cannot convert from 'int' to 'string?'  
   - **Code:** CS1503  
   - **File:** MainForm.cs  
   - **Line:** 207  
   - **Reason:** A nullable string parameter is being supplied with an integer value, causing a mismatch between expected and provided types.  
   - **Fix:** Convert the integer to a string before passing it in or adjust the method signature to accept an integer if that is required.

4. Cannot implicitly convert type 'string' to 'int'  
   - **Code:** CS0029  
   - **File:** MainForm.cs  
   - **Line:** 213  
   - **Reason:** A string value is being assigned to or returned from a member typed as `int`, so the compiler cannot perform the implicit conversion.  
   - **Fix:** Parse the string into an integer or change the receiving member's type to `string` if numeric conversion is not needed.

5. Cannot implicitly convert type 'int' to 'string'  
   - **Code:** CS0029  
   - **File:** MainForm.cs  
   - **Line:** 292  
   - **Reason:** An integer is being assigned to a variable or property typed as `string`, leading to a type mismatch.  
   - **Fix:** Convert the integer to its string representation (e.g., `value.ToString()`) or change the target member to type `int` if numeric handling is desired.

6. Cannot convert method group 'FoldDataIntoItem' to non-delegate type 'ItemData'. Did you intend to invoke the method?  
   - **Code:** CS0428  
   - **File:** MainForm.cs  
   - **Line:** 685  
   - **Reason:** The code is assigning the method group itself instead of the method result to an `ItemData` variable.  
   - **Fix:** Invoke the method by adding parentheses and required arguments (e.g., `FoldDataIntoItem(...)`) or adjust the variable type to store a delegate if that was intended.

7. The name 'spawnZoneTabPage' does not exist in the current context  
   - **Code:** CS0103  
   - **File:** MainForm.cs  
   - **Line:** 730, 745, 746  
   - **Reason:** The form code references a UI control named `spawnZoneTabPage` that is not declared in the designer or code-behind.  
   - **Fix:** Add the control definition to the designer file or rename the references to match the actual control identifier.

8. The name 'unitDataListBox' does not exist in the current context  
   - **Code:** CS0103  
   - **File:** MainForm.cs  
   - **Line:** 774, 778, 779, 780, 1092, 1094, 1095, 1107, 1175, 1177, 1188, 1213, 1389, 1466, 1468  
   - **Reason:** The form logic references `unitDataListBox`, but no field or control with that name is defined in the form's designer class.  
   - **Fix:** Create the `unitDataListBox` control in the designer or update the code to use the correct list box identifier that does exist.

9. The name 'selectedUnitLabel' does not exist in the current context  
   - **Code:** CS0103  
   - **File:** MainForm.cs  
   - **Line:** 782, 788, 789, 1136, 1143, 1147  
   - **Reason:** References to `selectedUnitLabel` are unresolved because the label is not declared in the accessible scope of `MainForm`.  
   - **Fix:** Declare the label control in the designer or adjust the code to reference the correct existing label.

10. The name 'unitNameInput' does not exist in the current context  
    - **Code:** CS0103  
    - **File:** MainForm.cs  
    - **Line:** 799, 803, 1112, 1119, 1125, 1153, 1158, 1183, 1194  
    - **Reason:** The code is interacting with `unitNameInput`, but the control is not defined in the designer or has a different name.  
    - **Fix:** Define the `unitNameInput` control in the designer or rename the usage to match the actual control name.

11. The name 'unitLevelSetter' does not exist in the current context  
    - **Code:** CS0103  
    - **File:** MainForm.cs  
    - **Line:** 813, 820, 1112, 1120, 1126, 1127, 1153, 1169, 1183, 1203  
    - **Reason:** The form references `unitLevelSetter`, but no such numeric control or field is defined, so the compiler cannot resolve it.  
    - **Fix:** Add the `unitLevelSetter` control to the designer file or update the code to use the correct control instance.

12. The name 'unitNotesInput' does not exist in the current context  
    - **Code:** CS0103  
    - **File:** MainForm.cs  
    - **Line:** 831, 837, 838, 1112, 1121, 1128, 1170, 1204  
    - **Reason:** There is no declaration of `unitNotesInput`, so attempts to access it fail at compile time.  
    - **Fix:** Declare the notes input control in the designer or update the references to the existing notes component.

13. The name 'createUnitButton' does not exist in the current context  
    - **Code:** CS0103  
    - **File:** MainForm.cs  
    - **Line:** 847, 851, 852  
    - **Reason:** The code expects a `createUnitButton` control, but it is missing from the designer-generated fields.  
    - **Fix:** Add the button to the designer with the correct name or rename the code references to match the actual button field.

14. The name 'updateUnitButton' does not exist in the current context  
    - **Code:** CS0103  
    - **File:** MainForm.cs  
    - **Line:** 854, 858, 859  
    - **Reason:** `updateUnitButton` is referenced in the code-behind, but no such button is defined, indicating a mismatch between code and designer.  
    - **Fix:** Define the button in the designer or update the code to reference the existing update button control.

15. The name 'deleteUnitButton' does not exist in the current context  
    - **Code:** CS0103  
    - **File:** MainForm.cs  
    - **Line:** 861, 865, 866  
    - **Reason:** A delete button named `deleteUnitButton` is referenced, but the designer file does not declare it.  
    - **Fix:** Add the delete button to the designer with the expected name or point the code to the actual button field.

16. The name 'spawnZoneListBox' does not exist in the current context  
    - **Code:** CS0103  
    - **File:** MainForm.cs  
    - **Line:** 897, 901, 902, 903, 1098, 1100, 1101, 1251, 1304, 1306, 1317, 1339, 1368, 1428  
    - **Reason:** `spawnZoneListBox` is referenced, but there is no corresponding control declared in the designer class.  
    - **Fix:** Declare the list box in the designer or align the code with the correct list box control name.

17. The name 'selectedZoneLabel' does not exist in the current context  
    - **Code:** CS0103  
    - **File:** MainForm.cs  
    - **Line:** 905, 911, 912, 1272, 1274  
    - **Reason:** The label `selectedZoneLabel` is missing from the form definitions, so the compiler cannot resolve it.  
    - **Fix:** Add the label control to the designer or update the references to the proper label instance.

18. The name 'spawnZoneNameInput' does not exist in the current context  
    - **Code:** CS0103  
    - **File:** MainForm.cs  
    - **Line:** 922, 926, 1268, 1282, 1287, 1312, 1323  
    - **Reason:** The form logic refers to `spawnZoneNameInput`, but this text box has not been declared in the designer class.  
    - **Fix:** Define the input control in the designer or change the code references to the actual control name.

19. The name 'spawnZoneNotesInput' does not exist in the current context  
    - **Code:** CS0103  
    - **File:** MainForm.cs  
    - **Line:** 937, 943, 944, 1264, 1269, 1298, 1332  
    - **Reason:** `spawnZoneNotesInput` is missing from the generated designer fields, causing unresolved references.  
    - **Fix:** Add the notes input control with the proper name or update the code to use the existing notes field.

20. The name 'createSpawnZoneButton' does not exist in the current context  
    - **Code:** CS0103  
    - **File:** MainForm.cs  
    - **Line:** 953, 957, 958  
    - **Reason:** The event handlers expect a button named `createSpawnZoneButton`, but the designer does not declare it.  
    - **Fix:** Add the button to the designer with the expected identifier or adjust the code to reference the actual button control.

21. The name 'updateSpawnZoneButton' does not exist in the current context  
    - **Code:** CS0103  
    - **File:** MainForm.cs  
    - **Line:** 960, 964, 965  
    - **Reason:** Code references `updateSpawnZoneButton`, yet the designer lacks a matching control declaration.  
    - **Fix:** Declare the update button in the designer or rename the references to match the existing control.

22. The name 'deleteSpawnZoneButton' does not exist in the current context  
    - **Code:** CS0103  
    - **File:** MainForm.cs  
    - **Line:** 967, 971, 972  
    - **Reason:** A delete button for spawn zones is referenced without a corresponding designer field.  
    - **Fix:** Add the button to the designer with the correct name or change the references to the actual delete button control.

23. The name 'spawnZoneAssignmentsListBox' does not exist in the current context  
    - **Code:** CS0103  
    - **File:** MainForm.cs  
    - **Line:** 1002, 1006, 1007, 1008, 1363, 1371, 1372, 1377, 1423, 1429, 1443  
    - **Reason:** The code attempts to populate `spawnZoneAssignmentsListBox`, but the list box is not declared.  
    - **Fix:** Define the assignments list box in the designer or update the code to use the existing control name.

24. The name 'assignmentMinimumSetter' does not exist in the current context  
    - **Code:** CS0103  
    - **File:** MainForm.cs  
    - **Line:** 1027, 1034, 1395, 1400, 1448, 1455, 1460, 1463  
    - **Reason:** The minimum setter control is referenced but not defined, leading to unresolved identifiers.  
    - **Fix:** Add the numeric input control to the designer or adjust the code to the correct control name.

25. The name 'assignmentMaximumSetter' does not exist in the current context  
    - **Code:** CS0103  
    - **File:** MainForm.cs  
    - **Line:** 1044, 1051, 1395, 1401, 1448, 1456, 1461, 1464  
    - **Reason:** References to `assignmentMaximumSetter` cannot resolve because the control is missing from the designer declarations.  
    - **Fix:** Define the maximum setter control in the designer or update the code to use the existing control.

26. The name 'assignUnitButton' does not exist in the current context  
    - **Code:** CS0103  
    - **File:** MainForm.cs  
    - **Line:** 1060, 1064, 1065  
    - **Reason:** The code references an `assignUnitButton` that is not defined, resulting in a missing identifier error.  
    - **Fix:** Add the assign button to the designer or correct the code to point to the existing button control.

27. The name 'removeAssignmentButton' does not exist in the current context  
    - **Code:** CS0103  
    - **File:** MainForm.cs  
    - **Line:** 1067, 1071, 1072  
    - **Reason:** `removeAssignmentButton` is referenced in event handlers, but the designer does not declare it.  
    - **Fix:** Declare the remove button in the designer or adjust the code to use the existing button name.

28. The name 'unitBindingSource' does not exist in the current context  
    - **Code:** CS0103  
    - **File:** MainForm.cs  
    - **Line:** 1089, 1094, 1174, 1206, 1220  
    - **Reason:** The binding source `unitBindingSource` is referenced without a corresponding field generated by the designer.  
    - **Fix:** Add the binding source to the designer or update the code to reference the actual binding component.

29. The name 'unitDefinitions' does not exist in the current context  
    - **Code:** CS0103  
    - **File:** MainForm.cs  
    - **Line:** 1089, 1173, 1219  
    - **Reason:** The data collection `unitDefinitions` is not declared in scope, so references cannot be resolved.  
    - **Fix:** Declare the `unitDefinitions` collection or replace it with the appropriate data source variable that does exist.

30. The name 'spawnZoneBindingSource' does not exist in the current context  
    - **Code:** CS0103  
    - **File:** MainForm.cs  
    - **Line:** 1090, 1100, 1302, 1334, 1347  
    - **Reason:** The code relies on `spawnZoneBindingSource`, but that binding source is not defined.  
    - **Fix:** Create the binding source in the designer or change the code to use the correct binding component.

31. The name 'spawnZoneDefinitions' does not exist in the current context  
    - **Code:** CS0103  
    - **File:** MainForm.cs  
    - **Line:** 1090, 1227, 1240, 1301, 1346  
    - **Reason:** `spawnZoneDefinitions` is referenced without a declaration in scope, indicating a missing field or property.  
    - **Fix:** Declare the `spawnZoneDefinitions` collection/property or reference the actual data structure that holds the spawn zone definitions.

32. The name 'assignmentBindingSource' does not exist in the current context  
    - **Code:** CS0103  
    - **File:** MainForm.cs  
    - **Line:** 1370, 1371, 1376, 1422, 1437  
    - **Reason:** The binding source for assignments is not declared, so the compiler cannot find `assignmentBindingSource`.  
    - **Fix:** Define the binding source in the designer or update the code to reference the existing assignments binding component.
