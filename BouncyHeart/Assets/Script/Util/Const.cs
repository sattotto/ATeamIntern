using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Const : MonoBehaviour {
	public static float[] SPEED = { 0.05f, 0.1f, 0.15f, 0.2f };

	public static int PLAYER_HP = 5;

	public static int ENEMY_HP = 3;

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
