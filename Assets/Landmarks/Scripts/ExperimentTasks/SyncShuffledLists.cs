using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SyncShuffledLists : ExperimentTask
{
    public bool tmp;
    public List<ObjectList> lists = new List<ObjectList>();
    private LM_PermutedList outList;


    public override void startTask()
    {
        TASK_START();

        // LEAVE BLANK
    }


    public override void TASK_START()
    {
        if (!manager) Start();
        base.startTask();

        try
        {
            outList = GetComponent<LM_PermutedList>();
        }
        catch (System.Exception ex)
        {
            Debug.LogError("You need to have an LM_PermutedList component attached to " + gameObject.name);
        }
        

        if (skip)
        {
            log.log("INFO    skip task    " + name, 1);
            return;
        }


        var randIndices = CreateShuffledIndex(lists[0].objects.Count);
        foreach (var ind in randIndices)
        {
            print(ind);
        }


        for (int ilist = 0; ilist < lists.Count; ilist++)
        {
            var oldList = lists[ilist];
            var newList = new List<GameObject>();
            foreach (var randind in randIndices)
            {
                newList.Add(oldList.objects[randind]);
            }

            var ol = new GameObject();

            ol.AddComponent<ObjectList>();
            var thing = Instantiate(ol, transform);
            thing.name = this.name + "_subset" + ilist;

            foreach (var entry in newList)
            {
                thing.GetComponent<ObjectList>().objects.Add(entry);
            }
            Destroy(ol);

            outList.permutedList.Add(newList);
        }

        //outList.Clear();

        //// WRITE TASK STARTUP CODE HERE
        //foreach (var list in lists)
        //{
        //    outList.Add(list.objects);
        //}

        //FisherYatesShuffle(outList);

        ////for (int ilist = 0; ilist < lists.Count; ilist++)
        ////{
        ////    lists[ilist].gameObject.GetComponent<ObjectList>().objects = outList[ilist];
        ////}

        //for (int i = 0; i < lists.Count; i++)
        //{
        //    var ol = new GameObject();

        //    ol.AddComponent<ObjectList>();
        //    var thing = Instantiate(ol, transform);
        //    thing.name = this.name + "_subset" + i;

        //    foreach (var entry in outList)
        //    {
        //        thing.GetComponent<ObjectList>().objects.Add(entry[i]);
        //    }
        //    Destroy(ol);
        //}
    }


    public override bool updateTask()
    {
        return true;

        // WRITE TASK UPDATE CODE HERE
    }


    public override void endTask()
    {
        TASK_END();

        // LEAVE BLANK
    }


    public override void TASK_END()
    {
        base.endTask();

        // WRITE TASK EXIT CODE HERE
    }

    public List<int> CreateShuffledIndex(int len)
    {
        var outIndices = Enumerable.Range(0, len);
        List<int> outlist = new List<int>();

        foreach (int oi in outIndices)
        {
            outlist.Add(oi);
        }

        FisherYatesShuffle(outlist);

        return outlist;
    }

    public static void FisherYatesShuffle<T>(IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

}