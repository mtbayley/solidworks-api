﻿using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;

namespace AngelSix.SolidDna
{
    /// <summary>
    /// Represents a SolidWorks model selection manager
    /// </summary>
    public class ModelSelectionManager : SolidDnaObject<SelectionMgr>
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public ModelSelectionManager(SelectionMgr model) : base(model)
        {

        }

        #endregion

        #region Selected Entities

        /// <summary>
        /// Get's all of the selected objects in the model
        /// </summary>
        /// <param name="action">The selected objects list to be worked on inside the action. NOTE: Do not store references to them outside of this action</param>
        /// <returns></returns>
        public void SelectedObjects(Action<List<SelectedObject>> action)
        {
            // Create list
            var list = new List<SelectedObject>();

            try
            {
                // Get selection count
                var count = mBaseObject.GetSelectedObjectCount2(-1);

                // If we have none, we are done
                if (count <= 0)
                {
                    action(new List<SelectedObject>());
                    return;
                }

                // Othewise, get all selected objects
                for (var i = 0; i < count; i++)
                {
                    // Get the object itself
                    var selected = new SelectedObject(mBaseObject.GetSelectedObject6(i + 1, -1));

                    // Get the type
                    selected.ObjectType = (swSelectType_e)mBaseObject.GetSelectedObjectType3(i + 1, -1);

                    // Add to the list
                    list.Add(selected);
                }

                // Call the action
                action(list);
            }
            finally
            {
                // Dispose of all items
                list.ForEach(f => f.Dispose());
            }

        }

        #endregion
    }
}