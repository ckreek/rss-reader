import { makeAutoObservable, runInAction } from "mobx";
import { api } from "utils";

export class RssItemStore {
  items: RssItem[] = [];

  constructor() {
    makeAutoObservable(this);
  }

  load = async () => {
    const items = await api.get<RssItem[]>("/rss");
    runInAction(() => {
      this.items = items;
    });
  };

  hide = async (item: RssItem) => {
    await api.post(`/rss/${encodeURIComponent(item.id)}/hide`)
    this.items = this.items.filter(x => x.id !== item.id);
  }
}

export interface RssItem {
  id: string;
  title: string;
  summary: string;
  url: string;
  publishDate: string;
}
