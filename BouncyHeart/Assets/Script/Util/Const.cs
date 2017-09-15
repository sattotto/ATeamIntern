using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Const : MonoBehaviour {
	// 移動スピード
	public static float[] SPEED = { 0.05f, 0.1f, 0.15f, 0.2f };

	// プレイヤーHP
	public static int PLAYER_HP = 1000;

	// 敵HP
	public static int ENEMY_HP = 500;

	// 敵攻撃力
	public static int ENEMY_ATK = 100;

	// ボール攻撃力
	// ボールのIDで変更
	// 0: i型 1: m型 2: o型
	public static int[] BALL_ATK = { 100, 250, 150 };

	// 持ち玉数
	public static int SHOOT_NUM = 5;

	// 基準となる位置
	public static Vector3 ENEMY_POS = new Vector3 (0, 4, 0);

	// index: 現在のステージ
	// 中身: ステージでの出現敵数
	public static int[] ENEMY_NUM = { 3 };
	// ステージごとの敵の位置
	public static float[] ENEMY_POS_X = { -1, 0, 1 };
	public static float[] ENEMY_POS_Y = { 0, 0.5f, 0 };
}
