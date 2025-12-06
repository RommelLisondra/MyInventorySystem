export interface SystemSetting {
  id: number;
  settingKey: string;
  settingValue?: string | null;
  description?: string | null;
  recStatus?: string | null;
}