PGDMP  %        	            |            Db-Techbiz-ERP    16.3    16.3     �           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            �           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            �           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            �           1262    16398    Db-Techbiz-ERP    DATABASE     �   CREATE DATABASE "Db-Techbiz-ERP" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'English_United States.1252';
     DROP DATABASE "Db-Techbiz-ERP";
                postgres    false            �            1259    16400    tm_employee_info    TABLE     �   CREATE TABLE public.tm_employee_info (
    id integer NOT NULL,
    name character varying(100),
    "position" character varying(100),
    department character varying(100),
    salary double precision
);
 $   DROP TABLE public.tm_employee_info;
       public         heap    postgres    false            �            1259    16399    employee_id_seq    SEQUENCE     �   CREATE SEQUENCE public.employee_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 &   DROP SEQUENCE public.employee_id_seq;
       public          postgres    false    216            �           0    0    employee_id_seq    SEQUENCE OWNED BY     K   ALTER SEQUENCE public.employee_id_seq OWNED BY public.tm_employee_info.id;
          public          postgres    false    215            P           2604    16403    tm_employee_info id    DEFAULT     r   ALTER TABLE ONLY public.tm_employee_info ALTER COLUMN id SET DEFAULT nextval('public.employee_id_seq'::regclass);
 B   ALTER TABLE public.tm_employee_info ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    215    216    216            �          0    16400    tm_employee_info 
   TABLE DATA           T   COPY public.tm_employee_info (id, name, "position", department, salary) FROM stdin;
    public          postgres    false    216   \       �           0    0    employee_id_seq    SEQUENCE SET     >   SELECT pg_catalog.setval('public.employee_id_seq', 22, true);
          public          postgres    false    215            R           2606    16405    tm_employee_info employee_pkey 
   CONSTRAINT     \   ALTER TABLE ONLY public.tm_employee_info
    ADD CONSTRAINT employee_pkey PRIMARY KEY (id);
 H   ALTER TABLE ONLY public.tm_employee_info DROP CONSTRAINT employee_pkey;
       public            postgres    false    216            �     x�MS�n�0}��_P��y�$M�M��)Zi_�d�;�U��wL�	cϹË��X�W�X��9��������D^K_��ZR��L �@�dA"⌆�(L�=jOR�.�� �£���|j���0k���8|�AV�U��
[阌=�����
�J:{	�E�4� >��՗����-��T�܊�,5O�,��-����'��,���k]JM�a�,�6Ym���[�\��7^�F�^n<�-i�%��TTZ�����s�T�1<�_l�ei�R�Xl�0=�R�$�Eq[����xV�:T��hN:�~�'q�L}	�3c�}�L�������##6h��9���g��5���x��Lbo>�5�@�w����z(O<�����I��Ѓe���b��7�iX�S»5�źU4�'Z.ŵz5s^�;֤�Q�y\���oy���;aI2pY��)�O�	a�w�;�/��XT�D^�o�|��.�k3�1�6^��> �L8�%]�����	�����$I�y���?�i�     