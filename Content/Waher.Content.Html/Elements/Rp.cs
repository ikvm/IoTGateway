﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Waher.Content.Html.Elements
{
	/// <summary>
	/// RP element
	/// </summary>
    public class Rp : HtmlElement
    {
		/// <summary>
		/// RP element
		/// </summary>
		/// <param name="Parent">Parent element. Can be null for root elements.</param>
		public Rp(HtmlElement Parent)
			: base(Parent, "RP")
		{
		}
    }
}
