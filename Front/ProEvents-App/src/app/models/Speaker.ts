import { EventSpeaker } from "./EventSpeaker";
import { SocialMedia } from "./SocialMedia";

export interface Speaker {
  id: number;
  name: string;
  resume: string;
  imageURL: string;
  phone: string;
  email: string;
  socialMedias: SocialMedia;
  eventSpeakers: EventSpeaker;
}
