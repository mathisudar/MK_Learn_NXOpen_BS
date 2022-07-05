


#------------------------------------------------------------------------------
# These imports are needed for the following template code
#------------------------------------------------------------------------------
import NXOpen
import NXOpen.BlockStyler
import NXOpen.UF

#------------------------------------------------------------------------------
# Represents Block Styler application cls
#------------------------------------------------------------------------------
class MK_ChangeFaceColor:
    # static class members
    theSession = None
    theUI = None
    # ------------------------------------------------------------------------------
    # Bit Option for Property: EntityType
    # ------------------------------------------------------------------------------
    EntityType_AllowFaces = 1 << 4
    EntityType_AllowDatums = 1 << 5
    EntityType_AllowBodies = 1 << 6
    # ------------------------------------------------------------------------------
    # Bit Option for Property: FaceRules
    # ------------------------------------------------------------------------------
    FaceRules_SingleFace = 1 << 0
    FaceRules_RegionFaces = 1 << 1
    FaceRules_TangentFaces = 1 << 2
    FaceRules_TangentRegionFaces = 1 << 3
    FaceRules_BodyFaces = 1 << 4
    FaceRules_FeatureFaces = 1 << 5
    FaceRules_AdjacentFaces = 1 << 6
    FaceRules_ConnectedBlendFaces = 1 << 7
    FaceRules_AllBlendFaces = 1 << 8
    FaceRules_RibFaces = 1 << 9
    FaceRules_SlotFaces = 1 << 10
    FaceRules_BossandPocketFaces = 1 << 11
    FaceRules_MergedRibFaces = 1 << 12
    FaceRules_RegionBoundaryFaces = 1 << 13
    FaceRules_FaceandAdjacentFaces = 1 << 14
    
    #------------------------------------------------------------------------------
    # Constructor for NX Styler class
    #------------------------------------------------------------------------------
    def __init__(self):
        try:
            self.theSession = NXOpen.Session.GetSession()
            self.theUI = NXOpen.UI.GetUI()
            
            ###[MK] Add Full Path
            
            self.theDlxFileName = "C:\MATHI\SPYDER\_NXOpen_BlockStyler\Python\BlockStyler\ChangeFaceColor\work\MK_ChangeFaceColor.dlx"
            
            ###[MK] Add Full Path
            
            
            self.theDialog = self.theUI.CreateDialog(self.theDlxFileName)
            self.theDialog.AddApplyHandler(self.apply_cb)
            self.theDialog.AddOkHandler(self.ok_cb)
            self.theDialog.AddUpdateHandler(self.update_cb)
            self.theDialog.AddInitializeHandler(self.initialize_cb)
            self.theDialog.AddDialogShownHandler(self.dialogShown_cb)
        except Exception as ex:
            # ---- Enter your exception handling code here -----
            raise ex
        
    
    # ------------------------------- DIALOG LAUNCHING ---------------------------------
   
   
    
    
    #------------------------------------------------------------------------------
    # This method shows the dialog on the screen
    #------------------------------------------------------------------------------
    def Show(self):
        try:
            self.theDialog.Show()
        except Exception as ex:
            # ---- Enter your exception handling code here -----
            self.theUI.NXMessageBox.Show("Block Styler", NXOpen.NXMessageBox.DialogType.Error, str(ex))
        
    
    #------------------------------------------------------------------------------
    # Method Name: Dispose
    #------------------------------------------------------------------------------
    def Dispose(self):
        if self.theDialog != None:
            self.theDialog.Dispose()
            self.theDialog = None
    
    #------------------------------------------------------------------------------
    # ---------------------Block UI Styler Callback Functions--------------------------
    #------------------------------------------------------------------------------
    
    #------------------------------------------------------------------------------
    # Callback Name: initialize_cb
    #------------------------------------------------------------------------------
    def initialize_cb(self):
        try:
            self.group0 = self.theDialog.TopBlock.FindBlock("group0")
            
            
            ###[MK] User specified ids
            
            self.id_face_select0 = self.theDialog.TopBlock.FindBlock("id_face_select0")
            self.id_colorPicker0 = self.theDialog.TopBlock.FindBlock("id_colorPicker0")
            
            ###[MK] User specified ids
            
            
        except Exception as ex:
            # ---- Enter your exception handling code here -----
            self.theUI.NXMessageBox.Show("Block Styler", NXOpen.NXMessageBox.DialogType.Error, str(ex))
        
    
    #------------------------------------------------------------------------------
    # Callback Name: dialogShown_cb
    # This callback is executed just before the dialog launch. Thus any value set 
    # here will take precedence and dialog will be launched showing that value. 
    #------------------------------------------------------------------------------
    def dialogShown_cb(self):
        try:
            # ---- Enter your callback code here -----
            pass
        except Exception as ex:
            # ---- Enter your exception handling code here -----
            self.theUI.NXMessageBox.Show("Block Styler", NXOpen.NXMessageBox.DialogType.Error, str(ex))
        
    
    #------------------------------------------------------------------------------
    # Callback Name: apply_cb
    #------------------------------------------------------------------------------
    def apply_cb(self):
        errorCode = 0
        try:
            # ---- Enter your callback code here -----
            
            
            ###[MK] callback codes
            
            self.ModifyColorOfFaces(self.GetSelectedFaces(),self.GetSelectedColor())
            
            ###[MK] callback codes
            
            
        except Exception as ex:
            # ---- Enter your exception handling code here -----
            errorCode = 1
            self.theUI.NXMessageBox.Show("Block Styler", NXOpen.NXMessageBox.DialogType.Error, str(ex))
        
        return errorCode
    
    #------------------------------------------------------------------------------
    # Callback Name: update_cb
    #------------------------------------------------------------------------------
    def update_cb(self, block):
        try:
            if block == self.id_face_select0:
                # ---- Enter your code here -----
                pass
            elif block == self.id_colorPicker0:
                # ---- Enter your code here -----
                pass
        except Exception as ex:
            # ---- Enter your exception handling code here -----
            self.theUI.NXMessageBox.Show("Block Styler", NXOpen.NXMessageBox.DialogType.Error, str(ex))
        
        return 0
    
    #------------------------------------------------------------------------------
    # Callback Name: ok_cb
    #------------------------------------------------------------------------------
    def ok_cb(self):
        errorCode = 0
        try:
            # ---- Enter your callback code here -----
            errorCode = self.apply_cb()
        except Exception as ex:
            # ---- Enter your exception handling code here -----
            errorCode = 1
            self.theUI.NXMessageBox.Show("Block Styler", NXOpen.NXMessageBox.DialogType.Error, str(ex))
        
        return errorCode
    
    
    #------------------------------------------------------------------------------
    # Function Name: GetBlockProperties
    # Returns the propertylist of the specified BlockID
    #------------------------------------------------------------------------------
    def GetBlockProperties(self, blockID):
        try:
            return self.theDialog.GetBlockProperties(blockID)
        except Exception as ex:
            # ---- Enter your exception handling code here -----
            self.theUI.NXMessageBox.Show("Block Styler", NXOpen.NXMessageBox.DialogType.Error, str(ex))
        
        return None
    
    
    
    
    ###[MK] Functions
    
    def GetSelectedColor(self):
        return self.id_colorPicker0.GetValue()[0]
    
    def GetSelectedFaces(self):
        faces=[]
        try:
            objects = self.id_face_select0.GetSelectedObjects()
            for obj in objects:
                if isinstance(obj,NXOpen.Face):
                    faces.append(obj)
        except Exception as ex:
            # ---- Enter your exception handling code here -----
            self.theUI.NXMessageBox.Show("Block Styler", NXOpen.NXMessageBox.DialogType.Error, str(ex))
        
        return faces
    
    def ModifyColorOfFaces(self, faces, color):
        if len(faces) > 0:
            dispMod = self.theSession.DisplayManager.NewDisplayModification()
            dispMod.NewColor = color
            dispMod.Apply(faces)
            dispMod.Dispose()  

    ###[MK] Functions

            
def main():
    theMK_ChangeFaceColor = None
    try:
        theMK_ChangeFaceColor =  MK_ChangeFaceColor()
        #  The following method shows the dialog immediately
        theMK_ChangeFaceColor.Show()
    except Exception as ex:
        # ---- Enter your exception handling code here -----
        NXOpen.UI.GetUI().NXMessageBox.Show("Block Styler", NXOpen.NXMessageBox.DialogType.Error, str(ex))
    finally:
        if theMK_ChangeFaceColor != None:
            theMK_ChangeFaceColor.Dispose()
            theMK_ChangeFaceColor = None
    
if __name__ == '__main__':
    main()

