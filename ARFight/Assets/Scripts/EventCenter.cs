using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//事件管理
public class EventCenter : Singleton<EventCenter> {

	private class EventOwner
	{
		public event EventListener mEvent;

		public void OnEventTrigger(params object[] args)
		{
            if (mEvent != null) mEvent(args);
		}
	}

	//事件监听
	public delegate void EventListener(params object[] args);

	//事件列表
	private SortedList<string, EventOwner> mEventList = new SortedList<string, EventOwner>();

    /**
	 * 抛送事件
	 * 
	 */
    public void PostEvent(UIEvent mEvent, params object[] args)
    {
        PostEvent(mEvent.ToString(), args);
    }

	/**
	 * 抛送事件
	 * 
	 */
	public void PostEvent(string regKey, params object[] args)
	{
		EventOwner mOwner;
		if (mEventList.TryGetValue (regKey, out mOwner)) {
			mOwner.OnEventTrigger(args);
		}
	}

	public bool Has(string regKey)
	{
		return mEventList.ContainsKey (regKey);
	}

    /**
	 * 注册事件监听
	 */
    public void RegisterEvent(UIEvent mEvent, EventListener listener)
    {
        RegisterEvent(mEvent.ToString(), listener);
    }

	/**
	 * 注册事件监听
	 */
	public void RegisterEvent(string regKey, EventListener listener)
	{
		EventOwner mEventOwner;
		
		if (mEventList.TryGetValue(regKey, out mEventOwner))
		{
			mEventOwner.mEvent += listener;
		}
		else
		{
            mEventOwner = new EventOwner();
            mEventOwner.mEvent += listener; 
			mEventList.Add(regKey, mEventOwner);
		}
	}

	/**
	 * 取消注册监听
	 * 
	 */
	public void UnRegisterEvent(string regKey)
	{
		mEventList.Remove (regKey);
	}

    public void UnRegisterEvent(UIEvent mEvent)
    {
        mEventList.Remove(mEvent.ToString());
    }

    /**
	 * 移除监听
	 */
    public void UnRegisterEvent(UIEvent mEvent, EventListener listener)
    {
        UnRegisterEvent(mEvent.ToString(), listener);
    }
    

	/**
	 * 移除监听
	 */
	public void UnRegisterEvent(string regKey, EventListener listener)
	{
		EventOwner mEventOwner;
		
		if (mEventList.TryGetValue(regKey, out mEventOwner))
		{
			mEventOwner.mEvent -= listener;
		}
	}
}
