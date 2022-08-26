import { makeAutoObservable, runInAction } from "mobx";
import { api } from "utils";

export interface Feed {
  id: number;
  name: string;
}

export const allFeed: Feed = {
  id: 0,
  name: "All",
};

export class FeedStore {
  feeds: Feed[] = [];
  selectedFeed: Feed = allFeed;

  constructor() {
    makeAutoObservable(this);
  }

  async load() {
    const feeds = await api.get<Feed[]>("/feed");
    runInAction(() => {
      this.feeds = feeds;
      this.select(allFeed);
    });
  }

  select(feed: Feed) {
    this.selectedFeed = feed;
  }
}
