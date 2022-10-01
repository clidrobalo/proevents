import { EventSpeaker } from "./EventSpeaker";
import { Lote } from "./Lote";
import { SocialMedia } from "./SocialMedia";

export interface Event {
  id: number;
  place: string;
  eventDate?: Date;
  theme: string;
  numberOfPerson: number;
  imageUrl: string;
  phone: string;
  lotes: Lote[];
  socialMedias: SocialMedia[];
  eventSpeakers: EventSpeaker[];
}

