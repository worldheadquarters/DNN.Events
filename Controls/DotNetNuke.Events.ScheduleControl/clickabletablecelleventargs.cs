using System;

#region Copyright
// 
// DotNetNuke® - http://www.dotnetnuke.com
// Copyright (c) 2002-2018
// by DotNetNuke Corporation
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
// documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
// the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
// to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions 
// of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.
//
#endregion


namespace DotNetNuke.Modules.Events.ScheduleControl
{
	
	/// -----------------------------------------------------------------------------
	/// Project	 : schedule
	/// Class	 : ClickableTableCellEventArgs
	///
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// The ClickableTableCellEventArgs class is used when the user clicks on an empty slot
	/// </summary>
	public sealed class ClickableTableCellEventArgs : EventArgs
	{
		
		private object _title;
		private object _rangeStartValue;
		private object _rangeEndValue;
		
		public ClickableTableCellEventArgs(object newTitle, object newRangeStartValue, 
			object newRangeEndValue)
		{
			this._title = newTitle;
			this._rangeStartValue = newRangeStartValue;
			this._rangeEndValue = newRangeEndValue;
		}
		
		public dynamic Title
		{
			get
			{
				return _title;
			}
		}
		
		public dynamic RangeStartValue
		{
			get
			{
				return _rangeStartValue;
			}
		}
		
		public dynamic RangeEndValue
		{
			get
			{
				return _rangeEndValue;
			}
		}
	}
	
	public delegate void ClickableTableCellEventHandler(object sender, ClickableTableCellEventArgs e);
	
}
