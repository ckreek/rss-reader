import { makeAutoObservable, runInAction } from "mobx";
import { api } from "utils";

export class FeedStore {
  feeds: Feed[] = [];
  selectedFeed: Feed | undefined;

  constructor() {
    makeAutoObservable(this);
  }

  async load() {
    const feeds = await api.get<Feed[]>("/feed");
    runInAction(() => {
      this.feeds = feeds;
      if (this.feeds.length > 0) {
        this.selectedFeed = this.feeds[0];
      }
    });
  }

  select(feed: Feed) {
    this.selectedFeed = feed;
  }
}

export interface Feed {
  id: number;
  name: string;
}
