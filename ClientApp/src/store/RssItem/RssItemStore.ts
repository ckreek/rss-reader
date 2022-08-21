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
}

export interface RssItem {
  id: string;
  title: string;
  summary: string;
  url: string;
  publishDate: string;
}
