using UnityEditor;
using UnityEngine;
public class ModelImport : AssetPostprocessor
{
    
    public void OnPreprocessModelx()
    {
        Debug.Log("calling here");
        ModelImporter modelImporter =

 (ModelImporter)assetImporter;

        if (modelImporter.isReadable)
        {

            modelImporter.isReadable = false;

            modelImporter.SaveAndReimport();

        }

    }


    public void MakeUnreadble()
    {

        ModelImporter importer = assetImporter as ModelImporter;
        string name = importer.assetPath.ToLower();
        Debug.Log($"Name : {name}");
        string extension = name.Substring(name.LastIndexOf(".")).ToLower();
        if (name.StartsWith("X"))
        {
            switch (extension)
            {
                case ".3ds":
                case ".fbx":
                case ".blend":
                    importer.globalScale = 1.0F;
                    importer.meshCompression = ModelImporterMeshCompression.Medium;
                    importer.optimizeMesh = true;
                    importer.importBlendShapes = false;
                    importer.addCollider = false;
                    importer.weldVertices = true;
                    importer.importVisibility = false;
                    importer.importCameras = false;
                    importer.importLights = false;
                    importer.isReadable = true;

                    importer.importNormals = ModelImporterNormals.Import;
                    importer.importTangents = ModelImporterTangents.CalculateMikk;

                    importer.importAnimation = false;
                    importer.animationType = ModelImporterAnimationType.None;
                    importer.generateAnimations = ModelImporterGenerateAnimations.None;

                    importer.importMaterials = true;
                    importer.materialLocation = ModelImporterMaterialLocation.External;
                    importer.materialName = ModelImporterMaterialName.BasedOnMaterialName;
                    importer.materialSearch = ModelImporterMaterialSearch.Everywhere;
                    break;
                default:
                    break;
            }
        }


    }

    void OnPreprocessModel()
    {
        ModelImporter importer = assetImporter as ModelImporter;
        string name = importer.assetPath.ToLower();
        Debug.Log($"Name that importing now : {name}"); 
        string extension = name.Substring(name.LastIndexOf(".")).ToLower();
        
            switch (extension)
            {
                case ".3ds":
                case ".fbx":
                case ".blend":
                    importer.globalScale = 1.0F;
                    importer.meshCompression = ModelImporterMeshCompression.Medium;
                    importer.optimizeMesh = true;
                    importer.importBlendShapes = false;
                    importer.addCollider = false;
                    importer.weldVertices = true;
                    importer.importVisibility = false;
                    importer.importCameras = false;
                    importer.importLights = false;
                    Debug.Log($"Make read {Measurement.makeReadable}");
                    importer.isReadable = Measurement.makeReadable;

                    importer.importNormals = ModelImporterNormals.Import;
                    importer.importTangents = ModelImporterTangents.CalculateMikk;

                    importer.importAnimation = false;
                    importer.animationType = ModelImporterAnimationType.None;
                    importer.generateAnimations = ModelImporterGenerateAnimations.None;

                    importer.importMaterials = true;
                    importer.materialLocation = ModelImporterMaterialLocation.External;
                    importer.materialName = ModelImporterMaterialName.BasedOnMaterialName;
                    importer.materialSearch = ModelImporterMaterialSearch.Everywhere;
                    break;
                default:
                    break;
            }
        
        
    }
}