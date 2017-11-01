// MIT License
// Copyright (c) 2016 Geometry Gym Pty Ltd

// Permission is hereby granted, free of charge, to any person obtaining a copy of this software 
// and associated documentation files (the "Software"), to deal in the Software without restriction, 
// including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, 
// and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, 
// subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all copies or substantial 
// portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT 
// LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. 
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, 
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE 
// SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Reflection;
using System.IO;
using System.ComponentModel;
using System.Linq;
using GeometryGym.STEP;

namespace GeometryGym.Ifc
{
	public partial class IfcUnitaryControlElement : IfcDistributionControlElement //IFC4  
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcUnitaryControlElementTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcUnitaryControlElementTypeEnum>(s.Replace(".", ""), out mPredefinedType);
		}
	}
	public partial class IfcUnitaryControlElementType : IfcDistributionControlElementType
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcUnitaryControlElementTypeEnum>(s.Replace(".", ""), out mPredefinedType);
		}
	}
	public partial class IfcUnitaryEquipment : IfcEnergyConversionDevice //IFC4
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcUnitaryEquipmentTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcUnitaryEquipmentTypeEnum>(s.Replace(".", ""), out mPredefinedType);
		}
	}
	public partial class IfcUnitaryEquipmentType : IfcEnergyConversionDeviceType
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcUnitaryEquipmentTypeEnum>(s.Replace(".", ""), out mPredefinedType);
		}
	}
	public partial class IfcUnitAssignment : BaseClassIfc
	{
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + ",(";
			if (mUnits.Count > 0)
			{
				str += ParserSTEP.LinkToString(mUnits[0]);
				for (int icounter = 1; icounter < mUnits.Count; icounter++)
					str += "," + ParserSTEP.LinkToString(mUnits[icounter]);
			}
			return str + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len) { mUnits = ParserSTEP.StripListLink(str, ref pos, len); }
		internal override void postParseRelate()
		{
			base.postParseRelate();
			mDatabase.Factory.Options.AngleUnitsInRadians = Math.Abs(1- getScaleSI(IfcUnitEnum.PLANEANGLEUNIT)) < 1e-4;
		}
	}
	public partial class IfcUShapeProfileDef : IfcParameterizedProfileDef
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleToString(mDepth) + "," + ParserSTEP.DoubleToString(mFlangeWidth) + "," + ParserSTEP.DoubleToString(mWebThickness) + "," + ParserSTEP.DoubleToString(mFlangeThickness) + "," + ParserSTEP.DoubleOptionalToString(mFilletRadius) + "," + ParserSTEP.DoubleOptionalToString(mEdgeRadius) + "," + ParserSTEP.DoubleOptionalToString(mFlangeSlope) + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "," + ParserSTEP.DoubleOptionalToString(mCentreOfGravityInX) : ""); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mDepth = ParserSTEP.StripDouble(str, ref pos, len);
			mFlangeWidth = ParserSTEP.StripDouble(str, ref pos, len);
			mWebThickness = ParserSTEP.StripDouble(str, ref pos, len);
			mFlangeThickness = ParserSTEP.StripDouble(str, ref pos, len);
			mFilletRadius = ParserSTEP.StripDouble(str, ref pos, len);
			mEdgeRadius = ParserSTEP.StripDouble(str, ref pos, len);
			mFlangeSlope = ParserSTEP.StripDouble(str, ref pos, len);
			if(release == ReleaseVersion.IFC2x3)
				mCentreOfGravityInX = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
}