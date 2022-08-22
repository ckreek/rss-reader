import { makeAutoObservable, runInAction } from "mobx";
import { api } from "utils";

export class FeedStore {
  feeds: Feed[] = [];
  selectedFeedId: number | undefined;

  constructor() {
    makeAutoObservable(this);
  }

  async load() {
    const feeds = await api.get<Feed[]>("/feed");
    runInAction(() => {
      this.feeds = feeds;
      if (this.feeds.length > 0) {
        this.selectedFeedId = this.feeds[0].id;
      }
    });
  }

  select(feedId: number) {
    this.selectedFeedId = feedId;
  }
}

export interface Feed {
  id: number;
  name: string;
}
