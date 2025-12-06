export interface SystemLog {
  id: number;
  logType: string;
  message: string;
  stackTrace?: string | null;
  loggedBy?: string | null;
  loggedDate: Date;
  recStatus?: string | null;
}