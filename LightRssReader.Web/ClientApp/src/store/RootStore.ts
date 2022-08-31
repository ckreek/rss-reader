import { makeAutoObservable } from "mobx";
import { RssPostStore } from "./RssPost/RssPostStore";
import { FeedStore } from "./Feed/FeedStore";

class RootStore {
  rssPostStore: RssPostStore;
  feedStore: FeedStore;

  constructor() {
    makeAutoObservable(this);

    this.rssPostStore = new RssPostStore();
    this.feedStore = new FeedStore();
  }
}

export const rootStore = new RootStore();

export const useRootStore = () => {
  return rootStore;
};
