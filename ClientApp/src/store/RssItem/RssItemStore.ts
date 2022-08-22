import { makeAutoObservable, runInAction } from "mobx";
import { api } from "utils";

type RssItemsByFeedId = {
  [feedId: number]: RssItem[] | undefined;
};

export class RssItemStore {
  itemsByFeedId: RssItemsByFeedId = {};

  constructor() {
    makeAutoObservable(this);
  }

  async load(feedId: number) {
    const items = await api.get<RssItem[]>(`/rss/${feedId}`);
    runInAction(() => {
      this.itemsByFeedId[feedId] = items;
    });
  }

  async hide(item: RssItem) {
    await api.patch(`/rss/${encodeURIComponent(item.id)}/hide`);
    runInAction(() => {
      const items = this.itemsByFeedId[item.feedId] || [];
      const filtered = items.filter((x) => x.id !== item.id);
      this.itemsByFeedId[item.feedId] = filtered;
    });
  }

  getByFeedId(feedId: number) {
    return this.itemsByFeedId[feedId] || [];
  }
}

export interface RssItem {
  id: number;
  title: string;
  summary: string;
  url: string;
  publishDate: string;
  feedId: number;
}
