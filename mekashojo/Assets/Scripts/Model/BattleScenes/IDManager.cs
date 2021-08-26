namespace Model
{
    /* IDについての設計
     * 各タイプのオブジェクトに対して、IDが何番まで使われたかはModel.IDManagerで管理する。
     * 各ビュークラスはAwakeメソッドでModel.IDManagerにアクセスして自身のIDを取得する。
     * その後Startメソッドで自身に対応するモデルクラスをnewし、コントローラークラスに用意
     * されているテーブルに、IDとモデルクラスのインスタンスが含まれる構造体を追加する。
     * コントローラークラスはそのテーブルに対してforeachをすることでモデルクラスのメソッドをUpdateメソッドで呼ぶ。
     * オブジェクトを削除する際は、ビュークラスにおいて、コントローラークラスのテーブルの
     * 対応する箇所を取り除き、その後自身をDestroyする。
     * ただしテーブルから対応する箇所を取り除く処理は、AddListenerの中に入れると
     * foreachのループの中で「ループに使っているテーブル」に変更を入れてしまい、
     * "Collection was modified; enumeration operation may not execute."
     * と言われるため、isBeingDestroyedなどのブール値を用意しておいて、Updateメソッドの中で行う。
     */
    public class IDManager
    {
        // IDを何まで使ったか
        public static int lastEnemyID = 0;
        public static int lastEnemyBulletID = 0;
        public static int lastPlayerBulletID = 0;
        public static int lastMaterialID = 0;
    }
}