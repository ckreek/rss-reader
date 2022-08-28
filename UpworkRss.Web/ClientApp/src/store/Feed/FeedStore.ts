import { makeAutoObservable, runInAction } from "mobx";
import { api } from "utils";

export interface Feed {
  id: number;
  name: string;
  url: string;
}

interface PostFeedDto {
  name: string;
  url: string;
}

export const allFeed: Feed = {
  id: 0,
  name: "All",
  url: "",
};

export class FeedStore {
  feeds: Feed[] = [];
  selectedFeed: Feed = allFeed;

  constructor() {
    makeAutoObservable(this);
  }

  async load(selectedId: number = allFeed.id) {
    const feeds = await api.get<Feed[]>("/feed");
    runInAction(() => {
      this.feeds = feeds;
      const selected = this.feeds.find((x) => x.id === selectedId) || allFeed;
      this.select(selected);
    });
  }

  select(feed: Feed) {
    this.selectedFeed = feed;
  }

  async add(dto: PostFeedDto) {
    const feed = await api.post<Feed>(`/feed`, dto);
    await this.load(feed.id);
  }

  async update(feedId: number, dto: PostFeedDto) {
    const feed = await api.put<Feed>(`/feed/${feedId}`, dto);
    await this.load(feed.id);
  }

  async delete() {
    if (this.selectedFeed.id !== allFeed.id) {
      await api.del(`/feed/${this.selectedFeed.id}`);
      await this.load();
    }
  }

  async restore(feedId: number) {
    if (feedId !== allFeed.id) {
      await api.post(`/feed/${feedId}/restore`);
      await this.load(feedId);
    }
  }

  getFeed(feedId: number) {
    return this.feeds.find((x) => x.id === feedId);
  }
}
