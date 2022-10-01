import { Speaker } from "./Speaker";

export interface EventSpeaker {
  eventId: number;
  event: Event;
  speakerId: number;
  speaker: Speaker;
}
