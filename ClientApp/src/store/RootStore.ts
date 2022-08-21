import { makeAutoObservable } from "mobx";
import { RssItemStore } from "./RssItem/RssItemStore";

class RootStore {
  rssItemStore: RssItemStore;

  constructor() {
    makeAutoObservable(this);

    this.rssItemStore = new RssItemStore();
  }
}

export const rootStore = new RootStore();

export const useRootStore = () => {
  return rootStore;
}
