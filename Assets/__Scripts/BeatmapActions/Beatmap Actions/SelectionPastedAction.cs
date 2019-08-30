﻿using System.Collections.Generic;
using UnityEngine;

public class SelectionPastedAction : BeatmapAction
{
    private List<BeatmapObjectContainer> pastedObjects = new List<BeatmapObjectContainer>();
    private List<BeatmapObject> pastedObjectsData = new List<BeatmapObject>();

    public SelectionPastedAction(List<BeatmapObjectContainer> pasted) : base(null)
    {
        pastedObjects = new List<BeatmapObjectContainer>(pasted);
        foreach (BeatmapObjectContainer container in pastedObjects)
            pastedObjectsData.Add(container.objectData);
    }

    public override void Undo(BeatmapActionContainer.BeatmapActionParams param)
    {
        foreach (BeatmapObjectContainer obj in pastedObjects)
        {
            param.bpm.DeleteObject(obj);
            param.notes.DeleteObject(obj);
            param.events.DeleteObject(obj);
            param.obstacles.DeleteObject(obj);
        }
    }

    public override void Redo(BeatmapActionContainer.BeatmapActionParams param)
    {
        Debug.Log("REDO ME YOU FUCK");
        pastedObjects.Clear();
        foreach (BeatmapObject obj in pastedObjectsData)
        {
            BeatmapObjectContainer recovered = null;
            switch (obj.beatmapType)
            {
                case BeatmapObject.Type.NOTE:
                    recovered = param.notes.SpawnObject(obj);
                    break;
                case BeatmapObject.Type.BOMB:
                    recovered = param.notes.SpawnObject(obj);
                    break;
                case BeatmapObject.Type.CUSTOM_NOTE:
                    recovered = param.notes.SpawnObject(obj);
                    break;
                case BeatmapObject.Type.OBSTACLE:
                    recovered = param.obstacles.SpawnObject(obj);
                    break;
                case BeatmapObject.Type.EVENT:
                    recovered = param.events.SpawnObject(obj);
                    break;
                case BeatmapObject.Type.CUSTOM_EVENT:
                    recovered = param.events.SpawnObject(obj);
                    break;
            }
            pastedObjects.Add(recovered);
        }
        SelectionController.SelectedObjects.Clear();
        SelectionController.SelectedObjects.AddRange(pastedObjects);
        SelectionController.RefreshSelectionMaterial(false);
    }
}