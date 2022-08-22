import { makeAutoObservable } from "mobx";
import { RssItemStore } from "./RssItem/RssItemStore";
import { FeedStore } from "./Feed/FeedStore";

class RootStore {
  rssItemStore: RssItemStore;
  feedStore: FeedStore;

  constructor() {
    makeAutoObservable(this);

    this.rssItemStore = new RssItemStore();
    this.feedStore = new FeedStore();
  }
}

export const rootStore = new RootStore();

export const useRootStore = () => {
  return rootStore;
}
