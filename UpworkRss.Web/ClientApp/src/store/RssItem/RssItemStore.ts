import { makeAutoObservable, runInAction } from "mobx";
import { ListResult } from "store/types/Common";
import { api } from "utils";

interface RssItemFilters {
  feedId: number;
  page: number;
}

type RssItemsByFeedId = {
  [feedId: number]: RssItem[] | undefined;
};
type NumberByFeedId = {
  [feedId: number]: number | undefined;
};

export class RssItemStore {
  itemsByFeedId: RssItemsByFeedId = {};
  pageByFeedId: NumberByFeedId = {};
  totalByFeedId: NumberByFeedId = {};
  loading = false;

  constructor() {
    makeAutoObservable(this);
  }

  async load(feedId: number) {
    this.loading = true;
    const page = this.getPageByFeedId(feedId) - 1;
    const result = await api.get<ListResult<RssItem>, RssItemFilters>(`/rss`, {
      feedId,
      page,
    });
    runInAction(() => {
      this.itemsByFeedId[feedId] = result.list;
      this.totalByFeedId[feedId] = result.total;
      this.loading = false;
    });
  }

  async reload(feedId: number) {
    this.itemsByFeedId[feedId] = [];
    this.load(feedId);
  }

  async hide(item: RssItem, feedId: number) {
    await api.patch(`/rss/${item.id}/hide`);
    await this.load(feedId);
  }

  async read(item: RssItem) {
    item.read = !item.read;
    await api.patch(`/rss/${item.id}/read`);
  }

  async setPage(feedId: number, page: number) {
    this.pageByFeedId[feedId] = page;
    this.itemsByFeedId[feedId] = [];
    await this.reload(feedId);
  }

  getItemsByFeedId(feedId: number) {
    return this.itemsByFeedId[feedId] || [];
  }

  getPageByFeedId(feedId: number) {
    return this.pageByFeedId[feedId] || 1;
  }

  getCountByFeedId(feedId: number) {
    const total = this.totalByFeedId[feedId] || 0;
    return Math.ceil(total / 10);
  }
}

export interface RssItem {
  id: number;
  title: string;
  summary: string;
  url: string;
  publishDate: string;
  feedId: number;
  read: boolean;
}
