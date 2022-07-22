
//==============================================================================
//  Purpose:  This TEMPLATE file contains C# source to guide you in the
//  construction of your Block application dialog. The generation of your
//  dialog file (.dlx extension) is the first step towards dialog construction
//  within NX.  You must now create a NX Open application that
//  utilizes this file (.dlx).
//
//  The information in this file provides you with the following:
//
//  1.  Help on how to load and display your Block UI Styler dialog in NX
//      using APIs provided in NXOpen.BlockStyler namespace
//  2.  The empty callback methods (stubs) associated with your dialog items
//      have also been placed in this file. These empty methods have been
//      created simply to start you along with your coding requirements.
//      The method name, argument list and possible return values have already
//      been provided for you.
//==============================================================================

//------------------------------------------------------------------------------
//These imports are needed for the following template code
//------------------------------------------------------------------------------
using System;
using NXOpen;
using NXOpen.BlockStyler;
using NXOpen.UF;

//------------------------------------------------------------------------------
//Represents Block Styler application class
//------------------------------------------------------------------------------
public class MK_ARC
{
    //class members
    private static Session theSession = null;
	private static UFSession theUfSession = null;
    private static UI theUI = null;
	
	public const double PI = 3.14;
	
    private string theDlxFileName;
    private NXOpen.BlockStyler.BlockDialog theDialog;
    private NXOpen.BlockStyler.Group group0;// Block type: Group
    private NXOpen.BlockStyler.DoubleBlock double0_startAngle;// Block type: Double
    private NXOpen.BlockStyler.DoubleBlock double01_endAngle;// Block type: Double
    private NXOpen.BlockStyler.DoubleBlock double02_radius;// Block type: Double
    private NXOpen.BlockStyler.SpecifyPoint point0;// Block type: Specify Point
    //------------------------------------------------------------------------------
    //Bit Option for Property: SnapPointTypesEnabled
    //------------------------------------------------------------------------------
    public static readonly int              SnapPointTypesEnabled_UserDefined = (1 << 0);
    public static readonly int                 SnapPointTypesEnabled_Inferred = (1 << 1);
    public static readonly int           SnapPointTypesEnabled_ScreenPosition = (1 << 2);
    public static readonly int                 SnapPointTypesEnabled_EndPoint = (1 << 3);
    public static readonly int                 SnapPointTypesEnabled_MidPoint = (1 << 4);
    public static readonly int             SnapPointTypesEnabled_ControlPoint = (1 << 5);
    public static readonly int             SnapPointTypesEnabled_Intersection = (1 << 6);
    public static readonly int                SnapPointTypesEnabled_ArcCenter = (1 << 7);
    public static readonly int            SnapPointTypesEnabled_QuadrantPoint = (1 << 8);
    public static readonly int            SnapPointTypesEnabled_ExistingPoint = (1 << 9);
    public static readonly int             SnapPointTypesEnabled_PointonCurve = (1 <<10);
    public static readonly int           SnapPointTypesEnabled_PointonSurface = (1 <<11);
    public static readonly int         SnapPointTypesEnabled_PointConstructor = (1 <<12);
    public static readonly int     SnapPointTypesEnabled_TwocurveIntersection = (1 <<13);
    public static readonly int             SnapPointTypesEnabled_TangentPoint = (1 <<14);
    public static readonly int                    SnapPointTypesEnabled_Poles = (1 <<15);
    public static readonly int         SnapPointTypesEnabled_BoundedGridPoint = (1 <<16);
    public static readonly int         SnapPointTypesEnabled_FacetVertexPoint = (1 <<17);
    public static readonly int            SnapPointTypesEnabled_DefiningPoint = (1 <<18);
    //------------------------------------------------------------------------------
    //Bit Option for Property: SnapPointTypesOnByDefault
    //------------------------------------------------------------------------------
    public static readonly int          SnapPointTypesOnByDefault_UserDefined = (1 << 0);
    public static readonly int             SnapPointTypesOnByDefault_Inferred = (1 << 1);
    public static readonly int       SnapPointTypesOnByDefault_ScreenPosition = (1 << 2);
    public static readonly int             SnapPointTypesOnByDefault_EndPoint = (1 << 3);
    public static readonly int             SnapPointTypesOnByDefault_MidPoint = (1 << 4);
    public static readonly int         SnapPointTypesOnByDefault_ControlPoint = (1 << 5);
    public static readonly int         SnapPointTypesOnByDefault_Intersection = (1 << 6);
    public static readonly int            SnapPointTypesOnByDefault_ArcCenter = (1 << 7);
    public static readonly int        SnapPointTypesOnByDefault_QuadrantPoint = (1 << 8);
    public static readonly int        SnapPointTypesOnByDefault_ExistingPoint = (1 << 9);
    public static readonly int         SnapPointTypesOnByDefault_PointonCurve = (1 <<10);
    public static readonly int       SnapPointTypesOnByDefault_PointonSurface = (1 <<11);
    public static readonly int     SnapPointTypesOnByDefault_PointConstructor = (1 <<12);
    public static readonly int SnapPointTypesOnByDefault_TwocurveIntersection = (1 <<13);
    public static readonly int         SnapPointTypesOnByDefault_TangentPoint = (1 <<14);
    public static readonly int                SnapPointTypesOnByDefault_Poles = (1 <<15);
    public static readonly int     SnapPointTypesOnByDefault_BoundedGridPoint = (1 <<16);
    public static readonly int     SnapPointTypesOnByDefault_FacetVertexPoint = (1 <<17);
    public static readonly int        SnapPointTypesOnByDefault_DefiningPoint = (1 <<18);
    
    //------------------------------------------------------------------------------
    //Constructor for NX Styler class
    //------------------------------------------------------------------------------
    public MK_ARC()
    {
        try
        {
            theSession = Session.GetSession();
			theUfSession= UFSession.GetUFSession();
			
            theUI = UI.GetUI();
			
			
			
            theDlxFileName = "C:\\MATHI\\NXOpenCS\\MK_Arc_CS_NXO_BS\\MK_ARC.dlx";
            theDialog = theUI.CreateDialog(theDlxFileName);
            theDialog.AddApplyHandler(new NXOpen.BlockStyler.BlockDialog.Apply(apply_cb));
            theDialog.AddOkHandler(new NXOpen.BlockStyler.BlockDialog.Ok(ok_cb));
            theDialog.AddUpdateHandler(new NXOpen.BlockStyler.BlockDialog.Update(update_cb));
            theDialog.AddInitializeHandler(new NXOpen.BlockStyler.BlockDialog.Initialize(initialize_cb));
            theDialog.AddDialogShownHandler(new NXOpen.BlockStyler.BlockDialog.DialogShown(dialogShown_cb));
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            throw ex;
        }
    }
    //------------------------------- DIALOG LAUNCHING ---------------------------------
    //
    //    Before invoking this application one needs to open any part/empty part in NX
    //    because of the behavior of the blocks.
    //
    //    Make sure the dlx file is in one of the following locations:
    //        1.) From where NX session is launched
    //        2.) $UGII_USER_DIR/application
    //        3.) For released applications, using UGII_CUSTOM_DIRECTORY_FILE is highly
    //            recommended. This variable is set to a full directory path to a file 
    //            containing a list of root directories for all custom applications.
    //            e.g., UGII_CUSTOM_DIRECTORY_FILE=$UGII_BASE_DIR\ugii\menus\custom_dirs.dat
    //
    //    You can create the dialog using one of the following way:
    //
    //    1. Journal Replay
    //
    //        1) Replay this file through Tool->Journal->Play Menu.
    //
    //    2. USER EXIT
    //
    //        1) Create the Shared Library -- Refer "Block UI Styler programmer's guide"
    //        2) Invoke the Shared Library through File->Execute->NX Open menu.
    //
    //------------------------------------------------------------------------------
    public static void Main()
    {
        MK_ARC theMK_ARC = null;
        try
        {
            theMK_ARC = new MK_ARC();
            // The following method shows the dialog immediately
            theMK_ARC.Show();
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            theUI.NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error, ex.ToString());
        }
        finally
        {
            if(theMK_ARC != null)
                theMK_ARC.Dispose();
                theMK_ARC = null;
        }
    }
    //------------------------------------------------------------------------------
    // This method specifies how a shared image is unloaded from memory
    // within NX. This method gives you the capability to unload an
    // internal NX Open application or user  exit from NX. Specify any
    // one of the three constants as a return value to determine the type
    // of unload to perform:
    //
    //
    //    Immediately : unload the library as soon as the automation program has completed
    //    Explicitly  : unload the library from the "Unload Shared Image" dialog
    //    AtTermination : unload the library when the NX session terminates
    //
    //
    // NOTE:  A program which associates NX Open applications with the menubar
    // MUST NOT use this option since it will UNLOAD your NX Open application image
    // from the menubar.
    //------------------------------------------------------------------------------
     public static int GetUnloadOption(string arg)
    {
        //return System.Convert.ToInt32(Session.LibraryUnloadOption.Explicitly);
         return System.Convert.ToInt32(Session.LibraryUnloadOption.Immediately);
        // return System.Convert.ToInt32(Session.LibraryUnloadOption.AtTermination);
    }
    
    //------------------------------------------------------------------------------
    // Following method cleanup any housekeeping chores that may be needed.
    // This method is automatically called by NX.
    //------------------------------------------------------------------------------
    public static void UnloadLibrary(string arg)
    {
        try
        {
            //---- Enter your code here -----
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            theUI.NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error, ex.ToString());
        }
    }
    
    //------------------------------------------------------------------------------
    //This method shows the dialog on the screen
    //------------------------------------------------------------------------------
    public NXOpen.UIStyler.DialogResponse Show()
    {
        try
        {
            theDialog.Show();
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            theUI.NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error, ex.ToString());
        }
        return 0;
    }
    
    //------------------------------------------------------------------------------
    //Method Name: Dispose
    //------------------------------------------------------------------------------
    public void Dispose()
    {
        if(theDialog != null)
        {
            theDialog.Dispose();
            theDialog = null;
        }
    }
    
    //------------------------------------------------------------------------------
    //---------------------Block UI Styler Callback Functions--------------------------
    //------------------------------------------------------------------------------
    
    //------------------------------------------------------------------------------
    //Callback Name: initialize_cb
    //------------------------------------------------------------------------------
    public void initialize_cb()
    {
        try
        {
            group0 = (NXOpen.BlockStyler.Group)theDialog.TopBlock.FindBlock("group0");
            double0_startAngle = (NXOpen.BlockStyler.DoubleBlock)theDialog.TopBlock.FindBlock("double0_startAngle");
            double01_endAngle = (NXOpen.BlockStyler.DoubleBlock)theDialog.TopBlock.FindBlock("double01_endAngle");
            double02_radius = (NXOpen.BlockStyler.DoubleBlock)theDialog.TopBlock.FindBlock("double02_radius");
            point0 = (NXOpen.BlockStyler.SpecifyPoint)theDialog.TopBlock.FindBlock("point0");
        
		// Set the upper-limits and lower-limits
		
			PropertyList arcStartAngleProps = double0_startAngle.GetProperties();
            arcStartAngleProps.SetDouble("MaximumValue", 360.0);
            arcStartAngleProps.SetDouble("MinimumValue", -360.0);
            arcStartAngleProps.SetDouble("Value", 45.0);
            arcStartAngleProps.Dispose();
            arcStartAngleProps = null;
			
			PropertyList arcEndAngleProps = double01_endAngle.GetProperties();
            arcEndAngleProps.SetDouble("MaximumValue", 360.0);
            arcEndAngleProps.SetDouble("MinimumValue", -360.0);
            arcEndAngleProps.SetDouble("Value", 180.0);
            arcEndAngleProps.Dispose();
            arcEndAngleProps = null;
			
			PropertyList arcRadiusProps = double02_radius.GetProperties();
            arcRadiusProps.SetDouble("MaximumValue", 10000.0);
            arcRadiusProps.SetDouble("MinimumValue", 0.001);
            arcRadiusProps.SetDouble("Value", 50.0);
            arcRadiusProps.Dispose();
            arcRadiusProps = null;
			
			
			
		
		}
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            theUI.NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error, ex.ToString());
        }
    }
    
    //------------------------------------------------------------------------------
    //Callback Name: dialogShown_cb
    //This callback is executed just before the dialog launch. Thus any value set 
    //here will take precedence and dialog will be launched showing that value. 
    //------------------------------------------------------------------------------
    public void dialogShown_cb()
    {
        try
        {
            //---- Enter your callback code here -----
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            theUI.NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error, ex.ToString());
        }
    }
    
    //------------------------------------------------------------------------------
    //Callback Name: apply_cb
    //------------------------------------------------------------------------------
    public int apply_cb()
    {
        int errorCode = 0;
        try
        {
            //---- Enter your callback code here -----
			
			Part workPart = theSession.Parts.Work;
			
			// Get --> theStartAngle
			PropertyList arcStartAngleProps = double0_startAngle.GetProperties();
			double theStartAngle = arcStartAngleProps.GetDouble("Value");
			arcStartAngleProps.Dispose();
            arcStartAngleProps = null;
			
			// Get --> theEndAngle
			PropertyList arcEndAngleProps = double01_endAngle.GetProperties();
			double theEndAngle = arcEndAngleProps.GetDouble("Value");
			arcEndAngleProps.Dispose();
            arcEndAngleProps = null;
			
			
			// Get --> theRadius
			PropertyList arcRadiusProps = double02_radius.GetProperties();
			double theRadius = arcRadiusProps.GetDouble("Value");
			arcRadiusProps.Dispose();
            arcRadiusProps = null;
			
			// Get --> theCenterPoint
			PropertyList theOriginProps = point0.GetProperties();
            Point3d theOriginPoint = theOriginProps.GetPoint("Point");
            theOriginProps.Dispose();
            theOriginProps = null;
			
			
			NXOpen.Point3d cenPt = theOriginPoint;
			NXOpen.NXMatrix arcMatrix = workPart.WCS.CoordinateSystem.Orientation;


			/* Fill out the data structure */
			
            double arcStartAngle = theStartAngle*(PI/180);
            double arcEndAngle = theEndAngle*(PI/180);
			double arcRadius = theRadius;


			NXOpen.Arc arc1;

			for (double i = 1; i <= 5; i++)
				{

					arc1 = workPart.Curves.CreateArc(cenPt, arcMatrix, arcRadius, arcStartAngle, arcEndAngle);

					arcRadius = arcRadius + 10;

				}

				
            
			
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            errorCode = 1;
            theUI.NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error, ex.ToString());
        }
		
		
        return errorCode;
    }
    
    //------------------------------------------------------------------------------
    //Callback Name: update_cb
    //------------------------------------------------------------------------------
    public int update_cb( NXOpen.BlockStyler.UIBlock block)
    {
        try
        {
            if(block == double0_startAngle)
            {
            //---------Enter your code here-----------
            }
            else if(block == double01_endAngle)
            {
            //---------Enter your code here-----------
            }
            else if(block == double02_radius)
            {
            //---------Enter your code here-----------
            }
            else if(block == point0)
            {
            //---------Enter your code here-----------
            }
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            theUI.NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error, ex.ToString());
        }
        return 0;
    }
    
    //------------------------------------------------------------------------------
    //Callback Name: ok_cb
    //------------------------------------------------------------------------------
    public int ok_cb()
    {
        int errorCode = 0;
        try
        {
            errorCode = apply_cb();
            //---- Enter your callback code here -----
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            errorCode = 1;
            theUI.NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error, ex.ToString());
        }
        return errorCode;
    }
    
    //------------------------------------------------------------------------------
    //Function Name: GetBlockProperties
    //Returns the propertylist of the specified BlockID
    //------------------------------------------------------------------------------
    public PropertyList GetBlockProperties(string blockID)
    {
        PropertyList plist =null;
        try
        {
            plist = theDialog.GetBlockProperties(blockID);
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            theUI.NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error, ex.ToString());
        }
        return plist;
    }
    
}
