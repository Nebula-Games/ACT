// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="GenericDelegates.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACT.Core.Delegates
{
    /// <summary>
    /// Generic Delegate Used Primarily to Determine When An Action Is completed.
    /// </summary>
    /// <param name="ReturnValue">Typical Usage Includes Passing An Action and An ID</param>
    public delegate void OnComplete(object[] ReturnValue);

    /// <summary>
    /// Generic Delegate Used Primarily to Determine When something has changed
    /// </summary>
    /// <param name="WhatChanged">The what changed.</param>
    public delegate void OnChanged(List<(string,object)> WhatChanged);


    /// <summary>
    /// On Communication Event
    /// </summary>
    /// <param name="Lines">The lines.</param>
    public delegate void OnCommunication(List<string> Lines);

    /// <summary>
    /// Generic Delegate used to determine Error Happenings
    /// </summary>
    /// <param name="ex">Current Exception Object</param>
    /// <param name="Args">Additional Arguments</param>
    public delegate void OnError(Exception ex, Dictionary<string, string> Args);

    /// <summary>
    /// Delegate OnMouseEvent
    /// </summary>
    /// <param name="Event">The event.</param>
    /// <param name="X">The x.</param>
    /// <param name="Y">The y.</param>
    /// <param name="UIElement">The UI element.</param>
    /// <param name="OldX">The old x.</param>
    /// <param name="OldY">The old y.</param>
    /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    public delegate bool OnMouseEvent(Enums.MouseEvents Event, int X, int Y, string UIElement, int OldX = 0, int OldY = 0);


    /// <summary>
    /// Used as a way to communicate menu item clicks.
    /// </summary>
    /// <param name="ItemName">Name of the item.</param>
    /// <param name="ID">The identifier.</param>
    /// <param name="OtherData">The other data.</param>
    public delegate void OnMenuItemClick(string ItemName, string ID, string[] OtherData = null);

    /// <summary>
    /// Used as a way to communicate menu item clicks.
    /// </summary>
    /// <param name="ID">Name of the item.</param>
    /// <param name="AdditionalData">The identifier.</param>
    public delegate void BeforeLoad(string ID, string AdditionalData);
}
