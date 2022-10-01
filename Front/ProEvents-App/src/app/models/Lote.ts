export interface Lote {
  id: number;
  name: string;
  price: number;
  startDate?: Date;
  endDate?: Date;
  quantity: number;
  eventId: number;
  event: Event;
}
