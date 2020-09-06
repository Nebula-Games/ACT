﻿// ***************************************************************
//  (c) $CREATED_YEAR$ - Mark Alicz.
//
// Author.........Mark Alicz
// Solution......: $SOLUTION$
// Project.......: $PROJECT$
// Filename......: $FILENAME$
//
// HISTORY
// **********************************************************************
///-------------------------------------------------------------------------------------------------
// namespace: ACT.Core.Types.Web
//
// summary:	.
///-------------------------------------------------------------------------------------------------

namespace ACT.Core.Types.Web
{
    ///-------------------------------------------------------------------------------------------------
    /// <summary>   An act combo box item. </summary>
    ///
    /// <remarks>   Mark Alicz, 7/25/2019. </remarks>
    ///-------------------------------------------------------------------------------------------------

    public class ACT_ComboBox_Item
    {
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the display text. </summary>
        ///
        /// <value> The display text. </value>
        ///-------------------------------------------------------------------------------------------------

        public string DisplayText { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the identifier. </summary>
        ///
        /// <value> The identifier. </value>
        ///-------------------------------------------------------------------------------------------------

        public string ID { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Returns The Display Text - Used For ComboBoxes. </summary>
        ///
        /// <remarks>   Mark Alicz, 7/25/2019. </remarks>
        ///
        /// <returns>   A string that represents the current object. </returns>
        ///-------------------------------------------------------------------------------------------------

        public override string ToString()
        {
            return DisplayText;
        }
    }
}